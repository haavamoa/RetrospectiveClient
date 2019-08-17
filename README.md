# **Retrospective Client**

[![Build Status](https://dev.azure.com/havardmoas/HaavamoaProjects/_apis/build/status/haavamoa.RetrospectiveClient?branchName=master)](https://dev.azure.com/havardmoas/HaavamoaProjects/_build/latest?definitionId=8&branchName=master)

![Retrospective Client][logo]

A windows client to use during a retrospective meeting.

Main features:

- Announce on Slack when the retrospective has started.
- Deliver an easy way of creating notes from the outcome of the meeting (positives, negatives and actions).
- Deliver a summary of the meeting on Slack.
- Configurable (Team Name, Announcement Message, etc.).

> See [releases](https://github.com/haavamoa/RetrospectiveClient/releases) to download latest release

## **Requirements**

- Windows.
- A Slack Webhook (required from your Slack administrator).
- .NET Core SDK.
- Nuke Build, see <https://nuke.build/docs/getting-started/setup.html>.

## **The idea**

The idea behind creating a application like this was that I got tired of keeping notes of my retrospectives. I used to start up my favorite editor, create three headers for positives notes, negatives notes, and actions and start taking notes when we were having the meeting. When the meeting was over, I used to save this locally and share it with my colleagues.

We finally started using Slack. My workflow was still somewhat the same, but now I did not have to save it locally, and we now had one place to share the notes afterwards.

After a while, I figured out that I wanted to try to make a application to write down my notes and that would publish a message before we started and a summary of my notes when we finished the meeting. This is the result.

## **How to run it**

I do not have a website for downloading latest version of the client yet, so you have to clone this repository in order to run it.

After cloning, do the following:

- Install nuke : `dotnet tool install Nuke.GlobalTool --global`
- Run `nuke`
  - Let it compile the solution
- Go to `\src\Retrospective.Clients.WPF\bin\Debug` and start `Retrospective.Clients.WPF.exe`
- Copy the folder `Debug` folder to whatever place you want and create a shortcut of the `.exe`

## **Screenshots / GIFs**

### *Startup screen to start a retrospective and the dialog to choose whether or not to announce the retrospective.Requires a nickname of the writer of the retrospective.*

![startingretro]

### *The Slack announcement after the retrospective has started*

![slackannouncement]

### *During the retrospective*

![duringretro]

### *Summary of the retrospective on Slack*

![retrosummary]

Remarks: The application might have changed since these screenshots / gifs were taken, but I hope that they provide a basic feeling of the flow of the application.
The gifs also have some awkward rendering issues.

[logo]: src/Retrospective.Clients.WPF/retrospective.ico
[startingretro]: doc/gif/StartingRetro.gif
[slackannouncement]: doc/img/SlackAnnouncement.png "The Slack message with the announcement"
[duringretro]: doc/gif/DuringRetro.gif
[retrosummary]: doc/img/RetroSummary.png "The Slack message with the retrospective summary"
