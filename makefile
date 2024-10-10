
csfiles:=$(shell fd cs$$)
csprojs:=$(shell fd csproj$$)
folders:=$(dir $(csprojs))

.phony:
	echo $(csfiles)
	echo $(csprojs)
	echo $(folders)

build:
	dotnet build

.SILENT:
format:
	@dotnet csharpier $(csfiles)
	@dos2unix $(csfiles)
	@dos2unix $(csprojs)
