﻿using System;
using Ono.MVP.Model;
using Ono.MVP.View;
using UniRx;
using UniRx.Triggers;
using Zenject;

namespace Ono.MVP.Presenter
{
    /// <summary>
    /// Presenter
    /// </summary>
    public class MusicPlayerPresenter : IInitializable, IDisposable
    {
        private readonly MusicPlayerView _musicPlayerView;
        private readonly MusicPlayerModel _musicPlayerModel;
        private CompositeDisposable _disposables;

        /// <summary>
        /// コンストラクタインジェクション
        /// </summary>
        /// <param name="musicPlayerView">View</param>
        /// <param name="musicPlayerModel">Model</param>
        public MusicPlayerPresenter(MusicPlayerView musicPlayerView, MusicPlayerModel musicPlayerModel)
        {
            _musicPlayerView = musicPlayerView;
            _musicPlayerModel = musicPlayerModel;
        }

        /// <summary>
        /// "MonoBehaviorで言うところのStart"にあたるライフサイクルイベントとして呼ばれるメソッド
        /// </summary>
        public void Initialize()
        {
            _disposables = new CompositeDisposable();

            //==========================
            // View → Modelへの反映
            //==========================

            //再生ボタン押下
            _musicPlayerView.PlayButton.OnClickAsObservable()
                .Subscribe(_ => { _musicPlayerModel.PlayMusic(_musicPlayerView.SeekBar.value); }).AddTo(_disposables);

            //停止ボタン押下
            _musicPlayerView.StopButton.OnClickAsObservable()
                .Subscribe(_ => { _musicPlayerModel.StopMusic(); }).AddTo(_disposables);

            //シークバーのドラッグ開始
            _musicPlayerView.SeekBar.OnPointerDownAsObservable()
                .Subscribe(_ => { _musicPlayerModel.StopMusic(); }).AddTo(_disposables);

            //シークバーのドラッグ終了
            _musicPlayerView.SeekBar.OnPointerUpAsObservable()
                .Subscribe(_ => { _musicPlayerModel.PlayMusic(_musicPlayerView.SeekBar.value); }).AddTo(_disposables);

            //==========================
            // Model → Viewへの反映
            //==========================

            //再生時間を反映
            _musicPlayerModel.MusicPlayTimeRP
                .Subscribe(time =>
                {
                    //シークバーに反映
                    _musicPlayerView.SeekBar.value = time;
                    //テキストに反映
                    _musicPlayerView.SetPlayTime(_musicPlayerModel.GetMusicTime());
                }).AddTo(_disposables);

            //再生モード変更に応じてボタンの表示を切り替え
            _musicPlayerModel.MusicPlayModeRP
                .SkipLatestValueOnSubscribe()
                .Subscribe(_ => { _musicPlayerView.SwitchButton(); }).AddTo(_disposables);
        }

        /// <summary>
        /// 破棄する処理
        /// </summary>
        public void Dispose()
        {
            _disposables.Dispose();
        }
    }
}