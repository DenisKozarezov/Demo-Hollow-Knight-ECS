<div align="center">

<h1>Hollow Knight ECS Demonstration</h1>

[About](#about-prototype)                     •
[Programming Patterns](#programming-patterns)    •
[Screenshots](#screenshots)                      •
[Project Status](#project-status)                •

</div>

## About Prototype

The purpose of the prototype is to work out the ***ECS*** architectural pattern on the example of the ```Hollow Knight``` metroidvania. All assets were downloaded from free sources, all rights belong to Team Cherry.

Controls:
1) Movement - ```WASD```.
2) Jump - ```Space```.
3) Attack - ```Left Mouse```.
4) Interact/Look/Talk - ```W```.
5) Healing - holding ```B```.

## Programming Patterns

The prototype uses the following architectural patterns:
1. ```ECS``` — we use a convenient and fast framework called [Entitas](https://github.com/sschmid/Entitas).
2. ```Behaviour Tree``` — custom framework with an editor to provide a simple AI. You can see our implementation in this [folder](https://github.com/DenisKozarezov/Demo-Hollow-Knight-ECS/tree/Entitas/Packages/com.korolev.uilityai-package). Double-click on Scriptable Object to open Behaviour Tree Editor window.

![image](https://user-images.githubusercontent.com/52127090/215850222-b1e44392-e260-4b36-88d1-59bca2fa2754.png)

3. ```Dependency Injection (Zenject)```.

## Screenshots

## Project Status

At the moment, the project is under development and is regularly being updated.
