# **Retrospective Client**

![Retrospective Client][logo]

A windows client to use during a typical retrospective meeting. 

Main features:

- Announce on Slack when the retrospective has started.
- Deliver a easy way of creating notes from the outcome of the meeting (positives, negatives and actions).
- Deliver a summary of the meeting on Slack.

## **Requirements**

- Windows.
- A Slack Webhook (required from you're Slack administrator).
- .NET Core SDK.
- Nuke Build, see <https://nuke.build/docs/getting-started/setup.html>

## **The idea**

The idea behind creating a application like this was that I got tired of keeping notes of my retrospectives. I used to start up my favorite editor, create three headers for positives notes, negatives notes and actions and start taking notes when we were having the meeting. When the meeting was over, I used to save this locally and share it with my colleagues.

We finally started using Slack. My workflow was still somewhat the same, but now I did not have to save it locally, and we now hade one place to share the notes afterwards.

After a while, I figured out that I wanted to try to make a application to write down my notes and that would publish a message before we started and a summary of my notes when we finished the meeting. This is the result.

## **How to run it**

I do not have a website for downloading latest version of the client yet, so you have to clone this repository in order to run it.

After cloning, do the following:

- Install nuke : `dotnet tool install Nuke.GlobalTool --global`
- Run `nuke`
  - Let it compile the solution
- Go to `\src\Retrospective.Clients.WPF\bin\Debug` and start `Retrospective.Clients.WPF.exe`
- Copy the folder `Debug` folder to whatever place you want and create a shurtcut of the `.exe`

[logo]: src/Retrospective.Clients.WPF/retrospective.ico 
