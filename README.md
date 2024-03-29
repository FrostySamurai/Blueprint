# Blueprint
A simple framework for small scale Unity projects. The main point is being able to access data from anywhere, tight coupling between objects,
and separating session data so that stuff like saving, loading, restarting the game etc. is simpler.

Architecture is split into to parts.

### Game
Global state of the game, stuff such ass application settings, scene loader, global event aggregator etc.
### Session
All in game logic (after starting new game, loading a save etc.) All session state is easy to clean, 
since it's contained in a single context object.

## Includes

### Start Up
A simple script that enables to always correctly start up the game in editor, no matter what scene you were editing.
You setup an initial scene (for me it's main menu) and then put a prefab containing this script into every scene.
You also put SceneLoader, EventSystem and StandaloneInputSystem on this object (with the latter two being disabled).

### Definitions
Definition system that supports definitions with string and int ids. These are currently required to reside inside
resources folder. They should be contained within a folder (for example Definitions) and this folder needs to be
specified in AppSettings.

### Event Aggregator
Event system that allows to register to event channels. Channels can be created by creating struct/class that inherits from IEvent interface.
It also supports parenting event aggregators to each other. The events propagate top-down, never bottom-up. This allows
session to have it's own event aggregator that still receives global events (for example from music manager or something)
without the worries of having a forgotten callback between sessions.

### Pooling
Simple component pool. Allows to save resources by not destroying dynamic objects and leaving them for later use.

### Saves
Saves are separated in sessions, which have their own save files (therefore you can have saves with the same name if
it is in different sessions).

When starting a new session, you specify session id. This could be for example a character name in an RPG game.
Optionally you can specify a save name which will start the session with loading the save file. Afterwards you can
access the saved state through Session.

Everything that needs saving has to implement ISavable interface. Every saved object needs a unique id and serializable
state object.

Save files are saved in the persistant files folder. The concrete subfolder and whether autosaves should be used can be
specified in AppConfig.

## TODO
- Improve readme by including example usage code