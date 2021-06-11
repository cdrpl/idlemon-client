# Idlemon Client

This is the Idlemon game client. It acts as the "front end" of the game, allowing players to interact with the server.

### Dependencies

- Unity3D (2021.1.12f1)

### Idlemon Server

The client will need access to a running instance of the [Idlemon server](https://github.com/cdrpl/idlemon-server).

### Project Setup

1. Install Unity Hub then install the version of Unity specified in the dependencies section.
2. Add the project in Unity Hub, when asked for the project folder select the [client](/client) folder.
3. Setup the Idlemon server by following the instructions in the [server README](/server/README.md).
4. Open [Const.cs](/client/Assets/Scripts/Const.cs) and set the SERVER variable to the correct value. (127.0.0.1 if the server is hosted locally)
5. Open the project in Unity, open the "Sign In" scene, and press play to run the game.

### Security Risk

The remember me functionality is currently unsafe. It stores the user password in plaintext using the PlayerPrefs class. This is just a simple implementation to make development easier.

### Credit

The button sound effect and some of the game art is taken from [kenny.nl](https://www.kenney.nl/) under the CC0 1.0 license.
