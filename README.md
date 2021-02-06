## MVPパターン

![overview image](https://github.com/ForJobOk/MVP_Demo/blob/master/MVP_Image.PNG)

```plantuml
@startuml
class CubeRotationPresenter {}
class SliderView {}
class CubeRotationModel {}

CubeRotationPresenter --> SliderView
CubeRotationPresenter --> CubeRotationModel
@enduml
```
