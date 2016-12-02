NDN Backprop Neural Net Trainer v1.0.1 README.TXT 
https://sourceforge.net/projects/ndnbackpropneur/
(2015-2-19)

NDN Backprop Trainer implements the backpropagation functionality subset of the open source NeuronDotNet object library in a generic user friendly application. All of the neural network functionality is provided by the NeuronDotNet library and hence a large credit must go to the author of that product.

Developers or others interested in the particulars of the neural network training options are advised to refer to the NeuronDotNet project which provides a nice help resource detailing its own features and general information relating to Neural Networks. Programmers in particular are encourged to visit that site for help in implementing their custom solutions.
https://sourceforge.net/projects/neurondotnet/

1. Creating a network
Your first step is to create a new network which is performed via the File menu and selecting the New option. The Create Network screen requires you to provide various parameters for your new network and importantly the data file which the network is trained against. Your data file must be comma separated and the input columns must precede the output column(s). You can specify how many of each column types exist though if the file only contains input and proceeding output column(s) then blank will suffice in the column counts. An initial heading can exist though it won't be saved in the internal binary format. The Cross Validation set is an optional selection which will allow you to measure your networks performance against a data set which is independant of that used to train the network.

2. Training the network
The Train button will begin training the network and this will proceed until the specified cycles have concluded or the user cancels the current training session. If a Cross Validation set (optional) is in use its own error will be charted against the primary data sets error. The interface will be less responsive when the CV set is used because the error calculation is performed in the foreground execution thread. Retraining is permitted as well as updating of the current training parameters. The Learning Rate in particular should be experimented with as values too high will not allow a descent to the current mimima. During training right-click the error graph to access control functions (e.g. zoom).

3. Saving and Loading network projects
A saved network consists of 3 separate files (4 with CV). The network and dataset files are saved as .Net binary serialized objects. In addition an XML file is saved with training parameters. Each file will begin with the project name and end with the appropriate extension for its type. To load a network ANY of these files can be selected and the application will search for all required files using the string preceeding the file extension as the project name.

4. Analyzing data files
Both the primary training set and CV set can be saved via the Options menu and will be saved with the forecasts predicted by the current network. Unfortunately any original headings will be replaced by generic titles. You will see Input, Output and Forecast titles each numbered in sequence.

5. Referencing your network in custom code (.Net)
As said before networks are saved as binary serialized .Net objects and hence need to be deserialized if to be used in custom code (this happens when you load a network in NDN Backprop Neural Net Trainer).

The following C# method is used in the application and will return the INetwork BackpropagationNetwork object stored at the given path.

	public static INetwork LoadNeuralNetwork(string path)
	{
		INetwork network;
		using (Stream stream = File.Open(path, FileMode.Open))
		{
			IFormatter formatter = new BinaryFormatter();
			network = (INetwork)formatter.Deserialize(stream);
		}
		return network;
	}

Again programmers are advised to look at the source code for the NeuronDotNet project as well as this one to assist with your custom solutions. 

(Note this application is provided with a slightly altered version of the NeuronDotNet library. Changes were necessary to permit re-training of existing networks without reinitialization of the saved weights. Also this version is targetting version 4 of the .Net framework.)