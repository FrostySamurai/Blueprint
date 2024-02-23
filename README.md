# Blueprint
A simple framework for small scale Unity projects.

Architecture is split into to parts.

### Game
Global state of the game, stuff such ass application settings, scene loader, global event aggregator etc.
### Session
All in game logic (after starting new game, loading a save etc.) All session state is easy to clean, 
since it's contained in a single context object.

## Includes

### Event Aggregator
Event system that allows to register to event channels. Channels can be created by creating struct/class that inherits from IEvent interface.
It also supports parenting event aggregators to each other. The events propagate top-down, never bottom-up. This allows
session to have it's own event aggregator that still receives global events (for example from music manager or something)
without the worries of having a forgotten callback between sessions.

### Pooling
Simple component pool.

## Planned

- Definitions

