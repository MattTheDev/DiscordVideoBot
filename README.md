[![.NET](https://github.com/MattTheDev/DiscordVideoBot/actions/workflows/dotnet.yml/badge.svg?branch=main)](https://github.com/MattTheDev/DiscordVideoBot/actions/workflows/dotnet.yml)

# Matt's Hacky Not So Great Discord Video Bot
Want to stream video via a Discord bot? This *kind of* makes that happen. Super hacky - but met my needs. Wanted to share.

1. Register a [Discord Application](https://discord.com/developers/applications#top) and generate a bot token.
2. [Install VLC](https://www.videolan.org/vlc/)
3. Launch VLC Player. 
4. In VLC, go to Tools -> Preferences -> Interface (first tab) -> Playlist and Instances (3rd section) -> Check `Allow only one instance`.
5. Close VLC Player.
6. Modify BotSettings.json, set preferred prefix, VLC Exe Path, and your bot token.

On launch of the bot, a VLC player will open.

7. In Discord, go to User Settings -> Activity Status -> Click Add It!
8. Choose VLC Player
9. In your server, click Go Live with VLC Player. Choose Voice Channel of your choosing.
10. Anyone can then type `yourPrefix video play YouTubeURL` and it'll launch that video through the VLC Player instance. 

NOTE: 

* I know this is hacky. Not a great solution. But it works.
* Have an issue? Need help? Feel free to post an issue above.
* If you close VLC Player, it will stop streaming VLC to the voice channel.
* If you mute VLC Player, it will no longer play sound through the voice channel.
