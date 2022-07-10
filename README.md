# hurley
If you'd like to target an older machine, your best bet would be
### [Hurley for .NET 4](https://github.com/videotoaster/hurley-dotnet4)
as you may be able to compile this for .NET 3? Haven't tested it myself.

### Precompiled?
[Hurley for .NET Core 3.1](https://github.com/videotoaster/hurley/releases)<br>
[Hurley for .NET Framework 4](https://github.com/videotoaster/hurley-dotnet4/releases)

Hurley plays music. I figured that's boring, so now it plays chiptune music out of the PC speaker. I thought
that was boring too, so I built Hurley up to both interpret, play, write, and create chiptune music written
in it's own native format. Right now, Hurley can read, write, play, and program music. It's incredibly simple
to use, yet not very powerful (1 channel audio?) and if you play beeps too fast, your speaker buzzes for some
reason. That's a hardware problem though.

To get started, download a Hurley release or compile it yourself, open the interpreter, and run
```
help
```
This command should print out a list of commands and how to use them. It should also give you instructions on
queueing tones to play.

`600,100` will queue a 600hz tone for 100ms. Running `play` after queueing a tone will play it. To save the
queue, run `save` and enter a file path. To clear the queue, run `nuke` and press Y. To load a queue from a
Hurley script, type `load` and then enter the path (can be relative, too!) to the script. `dump` will print
out a list of the commands in case you want to copy them to a text file yourself.

### Special note
This program hasn't been tested outside of Windows 8.1. I know `Console.Beep` works on 11, so 10 should work
too, but I don't know how .NET Core compatability works out on old Windows versions. Unfortunately, since
some Linux terminals are stupid, and I think the macOS one is too, this program probably won't work there.
If you'd like to port it to .NET Framework for older Windows machines, feel free!
