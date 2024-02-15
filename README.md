# UnityExtinguisher
Simulation of fire extinguisher operation in Unity 3D (Unity editor ver 2021.3.34f1)


**Task description:**

The fire extinguisher stands in front of the object that is on fire. 
When the player clicks on the bolt the fire extinguisher will be unlocked. Then the player is to click on the nozzle, which will trigger the nozzle positioning itself. Now the player can click on the handle to start extinguishing. 
The fire is supposed to be extinguishable if the player positions the extinguisher well (6s of extinguishing is supposed to put the fire out). The fire will "decrease" when it is extinguished and slowly increase when it stops being extinguished.

What is to be found on the UI:
- a slider that adjusts the extinguisher height
- show how much powder is left in the extinguisher (total is 10 seconds of extinguishing)
- the fire level


## Developer's Guide


### Scripts/

public class **AnimHintEvents** : MonoBehaviour

Attached to Extinguisher GameObject

Contains methods that starts by animation events. 

- void InitEvents() - initialization;
- public void BoltFallDown() - starts at the end of the "BoltOutAnim" animation: the Bolt begins to fall down;
- public void EnableHandle() - starts at the end of the "NozzleAnim" animation: enable the Nozzle Collider so a player can click on it;
- public void BoltGetOut() - starts at the start of the "BoltOutAnim" animation: plays a corresponding sound using AudioPlayer;
- public void ShowHint(string eventName) - starts at the end or at the start of several animations to show or hide a hint with "eventName"
- public void ShowSliderHint() - starts after using UI>SliderExtinguisher and shows hint of using the extinguishing handle if variable isExtingNeedPositioning is true
- public void HandleUpDown() - starts at the start of the "HandleUpAnim" and the "HandleDownAnim" animations: plays a corresponding sound using AudioPlayer;

public class [**AudioShotPlayer**](Assets/AudioShotPlayer/cs) : MonoBehaviour

		Attached to AudioShotPlayer GameObject

		Provides interface to play shot sounds

public class **BoltGetting**: MonoBehaviour, IPointerClickHandler

		Attached to Bolt GameObject (a child of Extinguisher GameObject)

		Handles events of the Bolt getting out.

 	- public void OnPointerClick(PointerEventData eventData) - handle a click on the Bolt Collider
	- public void OnCollisionEnter(Collision collision) - handle a Bolt collision with the Plane Collider

public class **ExtinguishingController** : MonoBehaviour
		Attached to Extinguisher GameObject
		Controls the process of extinguishing
	- void Init() - initialization;
	- Vector3 GetFirePosition() - finds a Burning Object and return position of the Fire.
  	- public void StartExtinguishing() - starts the extinguishing process.
   	- public void StopExtinguishing() - stops the extinguishing process.
   	- IEnumerator decreaseTime() - decreases the time of extinguishing, runs as a coroutine during the extinguishing process.
  	- void PowderRanOut() - disable the extinguishing process and shows a hint about powder lack. 
	- void StopWorking() - stops Powder streaming and stops the extinguishing time decreasing.
	- void setCoefExtinguishing() - counts the extinguishing efficiency coefficient which depends on the Fire and the Extinguisher positions
	- public float getCoefEstinguishing() - returns the extinguishing efficiency coefficient
   	- public float getPowderCount() - returns the value of the remaining extinguishing time until the powder is finished

public class **FireController** : MonoBehaviour
		Attached to BurningObject GameObject 
		Controls the fire level
  	- void Init() - initialization;
	- void Update() - updates the fire level and a volume of the fire sound 
	- void ChangeFireLevel() - changes the fire level which depends on the extinguishing process 	
	- void FireUp() - increases the fire level
	- void FireDown() - decreases the fire level
	- void FireStop() - stops the fire (Particle System, sound) and show a hint about this event
  	- public float getFireLevel() - return the fire level
   	- public float getMaxFireLevel() - return the maximum value of a fire level

public class **HandleUpDown**:  MonoBehaviour, IPointerDownHandler, IPointerUpHandler
		Attached to HandleTop GameObject (a child of Extinguisher>HandlePos GameObject)
		Handles a click on the HandleTop Collider to start or stop extinguishing

public class **SettingNozzle** : MonoBehaviour, IPointerClickHandler
		Attached to Nozzle GameObject (a child of Extinguisher GameObject)
   	- private void InitPoints() - saves initial points positions of the Hose
	- public void OnPointerClick(PointerEventData eventData) - runs the "NozzleAnim" animation
   	- private void UpdateHosePoints() - allows to lock the hose points so that the hose remains attached to the extinguisher 

public class UIController : MonoBehaviour
		Attached to UI GameObject with Canvas component
		Controls UI sliders to show correct values of the powder count (extinguishing time), the fire level and also correct the extinguisher position 

### Scripts/Hints/

public class **HintController** : MonoBehaviour

		Attached to HintController GameObject

		Provides interface to show and hide hints with the right language

public class **Hints** : ScriptableObject

		Allows to create objects containing data of hints and their event names.

		Provides iterface to get a hint from the dictionary using an event name as a key.  


### Animations/

	Animation component attached to the Extinguisher GameObject

- **BoltOutAnim** - The bolt is pulled out of the fire extinguisher.
- **NozzleAnim** - Positioning the nozzle in front of the extinguisher.
- **HandleUpAnim** - The extinguisher handle goes down.
- **HandleDownAnim** - The extinguisher handle goes up.


### Particle systems:

- **Powder** - GameObject containes a Particle System presenting a powder stream from the extinguisher 

	 Attached to Extinguisher>Nozzle

- **Fire** - 	 GameObject containes a Particle System presenting the fire 

	 Attached to BirningObject


### LineRenderer:

- **Hose** -   GameObject containes a Line Renderer component presenting the extinguisher hose (it connects the extinguisher with the nozzle)

	 Attached to Extinguisher>Nozzle  


### Free resources used in the project:

- https://www.turbosquid.com/3d-models/3d-extinguisher-model-1447524 - 3D model of an extinguisher
- https://free-sound-effects.net/ - sound effects









