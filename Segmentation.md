### pendahuluan

- perkenalan
- pendahuluan
- masang unity
- perkenalan layout
- setting project
- membuat setting image pertama di hierarcy

### coding part 1
- Quiz Manager (buat script baru QuizManager.cs)
- didalamnya, pertama coding `System.Serializable public class Question`, dan `System.Serializable public enum QuestionType`.
- buat fungsi SelectedQuestion()
- buat script baru `QuizUI`
- add field and properties pada `QuizUI`
- update isi di Unitynya

### coding part 2
- add fungsi Answer di `QuizManager`
- add fungsi `SetQuest` di `QuizUI`
- update di Unity
- add method `OnClick` di `QuizUI`
- ScriptableObject QuizDataScriptableObject

### unity update UI
- Update UI unity
- add MainMenu panel beserta buttonsnya
- update ui gameplayUI (topbar; score, time, lifepont)

### coding part 3
- update `QuizUI` ada text, list<Image>. GameObject(panel)
- update Awake in `QuizUI`
- add GameStatus
- update SatarGame in `QuizManager`
- update OnClick in `QuizUI`
- update on unity reference
- add GameOverPanel
- add QuizUIManager object, move component QuizUI to it

### coding part 4
- update on `QuizManager` adding topbar variable; score, time, lifetime
- updata fungsi  StartGame dan SelectQuestion in `QuizManager`
- add function `ReduceLife` on `QuizUI`
- update `Answer` fuction in `QuizManager`
- add SetTimer fuction and add Update fuction on `QuizManager`
- update QuizData variable on `QuizManager` to List instead
- add RetryButton Fucntion on `QuizUI`
- uodate unity reference

### finishing