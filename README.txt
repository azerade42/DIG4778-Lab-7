ObjectPool Pattern

The object pool pattern was implemented using a generic ObjectPool class.
It enqueues GameObjects of type T and game objects that implement this pattern can Get() to pull an object from the queue and must Return() to return objects to the pool.
The TargetController and ProjectileSpawner classes use this pattern to create several instances of these types at runtime while avoiding Instantiate/Destroy calls.

Builder Pattern

The builder pattern was implemented using the Target and TargetBuilder classes.
The abstract TargetBuilder class provides a Target property and several methods that build the properties of that property
The classes that inherit this builder must define methods that initialize these properties.


Observer Pattern

The observer pattern was implemented using the EventManager class as the Subject.
Controllers and managers from around the game subscribe to events inside of this class as observers.
The EventManager notifies the observers that subscribed to a specific event whenever one of the events is invoked.