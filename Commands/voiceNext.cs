using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using DSharpPlus.VoiceNext;
using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

namespace MinSon.Commands
{
   public class voiceNext : BaseCommandModule
    {


        [Command("VNstart")]
        public async Task JoinCommand(CommandContext ctx, DiscordChannel channel = null)
        {
            channel ??= ctx.Member.VoiceState?.Channel;
            if (channel != null)
                await channel.ConnectAsync();
            else
                await ctx.Channel.SendMessageAsync("You need to be in a voice channel for this command to work");
        }

        [Command("VNstop")]
        public async Task LeaveCommand(CommandContext ctx)
        {
            var vnext = ctx.Client.GetVoiceNext();
            var connection = vnext.GetConnection(ctx.Guild);
            await ctx.Channel.SendMessageAsync("Goodbye!");
            connection.Disconnect();
        }

        [Command("VNplay")]
        public async Task PlayCommand(CommandContext ctx, string path)
        {

            path = "D:/Stuff/MinSon/MinSon/bin/Debug/net5.0/Output/FREEDOM.mp3";

            //ProcessStartInfo startInfo = new ProcessStartInfo();
            //startInfo.FileName = @"D:/Stuff/MinSon/MinSon/bin/Debug/net5.0/Output/FREEDOM.ogg"; // Your absolute PATH 
            //var p = new Process();
            //p.StartInfo = new ProcessStartInfo(@"D:/Stuff/MinSon/MinSon/bin/Debug/net5.0/Output/FREEDOM.ogg")
            //{
            //    UseShellExecute = true
            //};
            //p.Start();
            
          //  VoiceNextConnection connection = await ctx.Channel.ConnectAsync();
            var vnext = ctx.Client.GetVoiceNext();

            var startInfo = new ProcessStartInfo
            {
                FileName = "ffmpeg",
                UserName = "MinSon",
                Arguments = $@"-i ""{path}"" -ac 2 -f s16le -ar 48000 pipe:1",
                RedirectStandardOutput = true,
                UseShellExecute = false
            };

            var ffmpeg = new Process();
            ffmpeg.StartInfo = startInfo;
                ffmpeg.Start();
            await ctx.Channel.SendMessageAsync(ffmpeg.ToString());
            Stream pcm = ffmpeg.StandardOutput.BaseStream;
            //VoiceTransmitSink transmit = ctx.Guild.Channels..GetTransmitSink();
          //  await pcm.CopyToAsync(transmit);

        }
        private System.IO.Stream ConvertAudioToPcm(string filePath)
        {
            var ffmpeg = Process.Start(new ProcessStartInfo
            {
                FileName = "ffmpeg",
                Arguments = $@"-i ""{filePath}"" -ac 2 -f s16le -ar 48000 pipe:1",
                RedirectStandardOutput = true,
                UseShellExecute = false
            });
            if (ffmpeg != null)
                return ffmpeg.StandardOutput.BaseStream;
            else
                return null;
        }
    }
}
