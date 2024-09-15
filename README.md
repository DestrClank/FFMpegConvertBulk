# FFMpegConvertBulk
# Overwiev
This little and simple tool converts a music folder into an other format by using FFmpeg. 
The tool will list every file in the folder that has the file extension specified on the arguments and uses FFmpeg to convert the files into the file format you want.

**Note** : You need to have FFMpeg into your environement variables into `PATH`.
**The tool uses .NET 8.0.**

# Usage
The command looks like this :
`FFMpegConvertBulk <input folder> <input file extension> <output file extension> [--overwrite]`

The tool will save the output files into `input folder\Converted`.
## Arguments 
- `input folder` : The folder containing the files you want to convert.
- `input file extension` : The input file extension/format that they have to look for.
- `output file extension` : The output file extension/format that you want the files to be converted.
## Options
- `--overwrite` : If any file already exists to `input folder\Converted`, it will overwrite it without asking.
## Example
`FFMpegConvertBulk "C:\SourceFolder" .at3 .mp3 --overwrite`

