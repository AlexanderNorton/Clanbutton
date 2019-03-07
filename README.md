# About Clanbutton 
www.clanbutton.com
Android/iOS app that allows gamers to team-up at the touch of a button.
To view this project's progress, feel free to check out our [Trello sprint board](https://trello.com/b/bvCITjii/sprint-board).

**Table of contents**
- [Clanbutton](#clanbutton)
  * [Overview](#overview)
  * [Development](#development)
    + [Structure](#structure)
    + [Trello Sprint Board](#trello-sprint-board)
    + [Development Tools](#development-tools)
  * [Development Blogs](#development-blogs)
    + [Alexander Norton](#alexander-norton)
    + [Ryan Byrne](#ryan-byrne)

## Overview
The Clanbutton makes game matchmaking super easy.
Sometimes, you may be playing games such as [Call of Duty](https://en.wikipedia.org/wiki/Call_of_Duty) or [Rainbow Six Siege](https://en.wikipedia.org/wiki/Tom_Clancy%27s_Rainbow_Six_Siege). These games automatically add you to a lobby with other random players. But there's a problem with this! The other players may not have a microphone or they may not speak your language. 

The Clanbutton fixes this by allowing you to search for a game and connect with other users who want to play the same game at your own choice. You will be able to view their profiles and see where they are from, if they use a microphone and other information such as the games they play or the game they are currently playing. All of this information will be pulled from their [Steam](https://en.wikipedia.org/wiki/Steam_(software)) account.

## Development
### Structure
*  Following a single sprint format for this project however we do meet up in person every week to have a 'stand up' to re-organize our to-do lists and ensure our sprint is in order.
* Meet with our project supervisor every one or two weeks.
* Implementing a continuous integration strategy so that all of our commits are tested on a pipeline through build to deployment.

### Trello Sprint Board
Our sprint board follows a breakdown - implementation - review strategy. This allows us to show our team mates the work we are currently working on 'breaking down' and 'implementing'. The review stage will show our team mates the code that needs to be reviewed before being merged to mainline.
If you would like to view our progress, feel free to check out our [Trello sprint board](https://trello.com/b/bvCITjii/sprint-board).

### Development Tools
The following tools will be used for the development of the Clanbutton:
* **[Firebase](https://firebase.google.com/) and the [Firebase API](https://firebase.google.com/docs/reference/)** is used for storing database information, starting user authentication and creating notifications.
* **C# programming language and .NET.** Programmed in C# we will be able to use Xamarin for Visual Studio and the .NET framework which is super powerful and has everything we need it to do to develop an Android or iOS application.
* **[Xamarin for Visual Studio](https://visualstudio.microsoft.com/xamarin/)** allows us to develop our Android application but also port it over to iOS should we ever decide to, avoiding too many complications and re-invention of the wheel.
* **[Visual Studio](https://visualstudio.microsoft.com/)** for our main IDE for this project (also allows a quick and easy use of Git)

# Project
## Video walk-through

## User Guide

### [Clanbutton User Guide](https://gitlab.computing.dcu.ie/nortona5/2019-ca326-clanbutton/blob/master/Documentation/Clanbutton%20User%20Guide.pdf)

## Development Blogs

### Alexander Norton

**Alexander Norton, Blog.**
(Monday 28th of January - Friday 1st of February)
This week I added the implementation of a user's profile and how it obtains information from the database. There is a function called 'GetAccount' which will pull information from the Firebase database such as their username, email, or any other information we add to the 'UserAccount' class. It also means that when the current user needs to get information about another user, such as when they access another user's profile, we can easily get the Firebase UserId for that user, call GetAccount(UserId), and use all of that information for whatever we need it for.
Next week I'll start implementing the Steam API log in system as we currently only allow login through an email address (that may or may not exist - it doesn't matter and that's the problem).

(Monday 4th of February - Friday 8th of February):
This week I worked on the implementation of the Steam API. I also ensured users will stay logged in when they leave the app or can log out of the app.
Users now have the ability to log in to their account via Steam allowing their Clanbutton profiles to be filled with their Steam information, instead of inputting it themselves. This means that when we start working on the users' profile system, their profile picture, country, games they play, current game being played, etc. can all be added very easily to their profile. This is actually my favorite part about this app - the simplicity of registering and finding a team mate 'at the touch of a button'.
Next week I'll start working on setting up a test pipeline and allow the app to be built and tested on each commit made to the repository.

(Monday 11th of February - Friday 15th of February):
I ran into some trouble when implementing the test pipeline. Setting up a runner is no problem however we would like the runner to run on a server (AWS/Azure) so that the build is not depending on a runner on the local machine.
This means that we can build/test/deploy using a runner on a server instead. It's also a great opportunity to learn something new. But the issue I'm having is c# and Xamarin must be installed on these machines and we're having some incompatible dependency issues.
We'll be leaving this to be implemented in the future when we begin writing tests but it's great to keep note of.
I also began working on the user profiles which will receive the user's information from the Steam API including your profile picture.

(Monday 18th of February - Friday 22nd of February):
Made some considerable progress on the user profiles. Game searching is now implemented so you can now search for a game and match up with other users who want to play the same game. It's a little buggy at the moment, but you can click their profile picture and it will take you to their profile page and download their profile picture and user information.
It's also super slow - taking a look into caching for next week to see what we can do about ensuring we don't have to download pictures we've already downloaded or make unnecessary calls to the database that were already made (such as getting a user's account)

(Monday 25th of February - Friday 1st of March):
Although buggy, we can now join chat rooms and send each other messages. This is done by sending the chat messages to Firebase and being read by a listener in the messaging activity.
Caching has also been implemented which has made the start up time and load times much faster.

(Monday 4th of March - Friday 8th of March):
The app is now a functioning app - matching you with users who want to play the same game, with user profiles and the ability to send messages to each other. You can also deploy beacons (notifications would be nice but we don't have the time to implement them) and activities appear on the front page of the app.
We also implemented a really nice library feature which collects the games from your Steam library and places them under the search bar as game suggestions.
We really enjoyed working on the project over the past couple of weeks and we're looking forward to placing it on the Google Play Store to get some feedback from a number of users.

### Ryan Byrne