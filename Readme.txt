CSharpFileDisplaySample Sample
==============================

DESCRIPTION:

This sample demonstrates two file display API features. Both of them allow you to display a part, assembly and drawing 
file and able to manipulate views of the file being displayed outside of Inventor. Both features are part of the Inventor 
Apprentice server. C# is the language used to program the stand alone application.

The first feature is using a picture box as a place holder where the file is going to displayed. 
InventorApprentice.ApprenticeServerComponent object is used to open a ipt/iam/idw file. The picture box's handle is added 
to the ClientViews and returns an InventorApprentice.ClientView object. With access to the InventorApprentice.Camera object, 
the desired view orientation type is set. Finally, InventorApprentice.ClientView.Update is called and the file image is d
isplayed just as it is displayed in Inventor.

There are some mouse events, OnMouseDown, OnMouseUp and OnMouseMove, need to be handled so the file view can be manipulated. 

The second feature is to encapsulate most of the first feature into an ActiveX control - InventorViewCtrl.ocx. Once the control 
is registered and placed into the form, all you have to do is to set the FileName property to be the file you want to display. 
Mouse events handling and view manipulation is all built-in. 

A debug build of the EXE and 3 dependent DLLs are provided with the sample, they are located at the ..\bin\Release\ sub folder. 
The EXE is dependant on Microsoft .NET Framework.

Note that for assembly files, occurrence specific visibility and color are not supported when displaying an assembly file that 
has these settings.

LANGUAGE/COMPILER: VC# (.net)


REQUIREMENTS:

This sample requires the following: 

1. Inventor server.
2. Microsoft .NET Framework installed to run the application.
3. To be able to compile the application, Visual Studio.NET has to be installed.


INSTRUCTIONS:

To compile this sample, start Visual Studio.NET, open CSharpFileDisplaySample.sln file and make sure the references are correct. 
Build the solution.

To run this sample, execute CSharpFileDisplaySample.exe, open a IPT, IAM or IDW file.

Register the following files using regsvr32.exe: 
Make sure InventorViewCtrl.ocx is registered. If not, run regsvr32.exe at the command line and use InventorViewCtrl.ocx as the argument.
