# Real-Time AR Coach!

This software gives real-time haptic feedback to athletes from a video stream. In this proof of concept, the badminton player receives technique guidance while he is stroking.

When the player strikes, the haptic feedback sensors gives an impulse to the player's joint, if the joint has been in a nonoptimal position. So, the player can improve his/her stroking technique without stopping the game.

This is made possible by using AR skeleton tracking, LTE-M networks and IoT device with haptic feedback sensors.

The same technique could also be applied to other sports such as golf, dancing, boxing, wall climbing etc.

## Description

The software gives feedback from the badminton strokes by using haptic feedback sensors. To start using this application, set the camera phone towards the badminton court. Place the haptic feedback sensors to your arm.

The mobile app starts recording the user and his/her hand movements. The captured hand movements are streamed to the server.

The server detects the strokes and compares them to a beforehand recorded model strokes. If the user's stroke does not match to the model strokes, the server sends the vibration to the haptic feedback sensors.

The haptic feedback sensors guide the hand movements in the right direction. If there is no vibration, the stroke has been successful.


![Skeleton Tracking](https://github.com/Frans-L/iothon-ar-skeleton/blob/master/Images/AR_Tracking.jpg?raw=true "Skeleton Tracking")


^ The skeleton tracking

![IoT](https://github.com/Frans-L/iothon-ar-skeleton/blob/master/Images/IoT.jpg?raw=true "IoT")

^ Our IoT device with LTE-M connection and haptic feedback sensors

![Server](https://github.com/Frans-L/iothon-ar-skeleton/blob/master/Images/Server.jpg?raw=true "Server")

^ Fitting the hand motions to the model data

## Structure

The software consists of 3 different parts.

* **Android app** 
    * The app captures the badminton court and detects the player's skeleton. The app streams the data of body joints to the server over UDP. The app is made with Unity and Huawei AR Engine.
    * https://github.com/Frans-L/iothon-ar-skeleton
* **Server**
    * The server normalizes the data, so the height of the user nor the position on the court are not affecting the data. The data is compared to the pre-recorded model strokes. If the current arm position differs from the optimal position, the server sends the data to our IoT sensor. The server is made with Python3.
    * https://github.com/MythicManiac/RacketServer
* **Haptic Feedback IoT**
    * The proof of concept IoT device is Arduino with eight small haptic feedback sensors. The Arduino fetches the orders from the server and vibrates the sensors when needed. 
    * https://github.com/Perdex/Iothon_haptic_feedback

## Requirements

* Android app
    * Unity 2018.3.
    * Huawei AR CORE
    * The latest Huawei mobile phone, e.g. P20 Mate
    * Android JDK 1.8
* Server
    * Docker
    * Python3
* Haptic Feedback IoT
    * Arduino xxx
    * IoT Sim card: LTE-M
    * ...

## Vision

The technique trainings of sports have been dependent on professional trainers for decades. We really think that this idea could made technique training possible for everyone with truly small expenses. The potential market is huge.

In addition, the live haptic feedback is something that we have not seen anyone to do before.

If we would have more time than 48 hours, we would add more stroke models and make the hapti feedback sensor to be smaller.

If you are interested of the idea, feel free to contact us. We are more than happy to share our vision.

## Info

This proof of concept is made in 48 hours at [IoThon 2019](https://iothon.io/) 3.5. - 5.5.2019.

* **Perttu Y** [Perdex](https://github.com/Perdex) 
* **Frans L** [Frans-L](https://github.com/Frans-L)
* **Joa R** [JoaRiski](https://github.com/JoaRiski)
* **Joonatan B** [Joonardo](https://github.com/Joonardo)

## License

[The MIT License](https://opensource.org/licenses/MIT)