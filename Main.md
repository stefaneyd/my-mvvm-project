**Table of Contents**



# Introduction #
In this module we are going to discuss the ModelViewViewModel otherwise known as the MVVM design pattern. The ModelViewViewModel design pattern facilitates modern development techniques such as separation of concerns, unit testing and test driven development. As with most design patterns the ModelViewViewModel design pattern is just a set of guidelines that when you follow them it makes it easier to write applications. Use them where they makes sense and modify them to fit the needs of your application at a given point in time.
  * Where did MVVM come from?
  * What is MVVM?
  * Components of MVVM
  * Implementation

# History #
  * 2004 Martin Fowler - Presentation Model (PM)
    * Separates a view from it's state and behavior
    * Not dependent on a specific UI framework
In 2004 a gentleman named Martin Fowler published an article that describes a design pattern called the presentation model. In this article he explains that the Presentation Model design pattern is similar to the MVP or the Model View Presenter pattern in that it separates a view from its behavior and state. Specifically the Presentation Model pulls the state and behavior out of the view and into a model class. It coordinates with the domain layer and provides an interface to the view. The presentation model frequently updates its view so that the two stay in sync with each other, that synchronization logic exists as code in the presentation model classes. One thing to note about the presentation model pattern, it is not specific to any UI framework.
  * 2005 John Gossman unveiled the MVVM pattern
    * Variation of MVC pattern
In 2005 a gentleman named John Gossman, who at the time was one of the WPF and Silverlight architects at microsoft, unveiled the ModelViewViewModel pattern in his blog. In the blog he described the ModelViewViewModel as a variation of the Model View Controller but its tailored for the modern UI development platforms where the view is the responsibility of a designer rather than a developer.
  * 2008 John changes his mind
    * Identical to PM pattern
    * Dependent on WPF/Silverlight
In 2008 John changes his mind, he realised that the ModelViewViewModel pattern is identical to Fowlers Presentation Model, and that both patterns feature an abstraction of a view which contains a view state and behavior. Fowler introduced the Presentation Model pattern as a means of creating a UI platform independent abstraction of a view, where as Gossman introduced the ModelViewViewModel pattern as a standardised way to leverage core features of WPF and Silverlight. Meaning that the ModelViewViewModel design pattern is tailor made and specific to the WPF and Silverlight platforms.

# Intent #
  * Separate concerns
    * View
    * View's state and behavior
    * Data
  * Unit Testing & UI Testing
  * Maintenance
  * Extensibility
  * Enables the designer/developer workflow
  * Take advantage of WPF/Silverlight data binding
So what exactly does the ModelViewViewModel design pattern really do for us, for one it helps the developers separate the concerns of the view the view state and behavior and the underlying data. It also allows for unit testing of the application as well as UI testing. Most would agree that maintenance is the number one cost in the software development lifecycle, add a new feature here, add a new feature there, make a change, modify this, modify that. Well we want to be able to do that throughout the entire lifecycle of the application but we don't want to break anything and we want to make it easy to actually implement these features or make those changes. It also helps developers and designers work together with less technical difficulties, in the past there has always been friction between the designers and the developers, you know who is touching my UI and who is touching my code. Well now you can split the two teams more easily, you don't get a lot of that budding heads that you used to get, you know designers can go and create this awesome beautiful user interface without ever having to know what the developer is doing, the designer just needs to know the data points that they are going to exposing to the user and the programmer doesn't have to know what the designer is doing, he doesn't care he is just coding to the functionality of the applications requirements. The single most important aspect that the ModelViewViewModel pattern relyse on is the data binding infrastructure in WPF and Silverlight.By binding elements of a view to a viewModle you get very loose coupling between the two, and your entirely remove the need for writing code in a views code behind or in a viewModel that would directly update a view. The data binding system also supports input validation, which provides a standardized way of transmitting validation errors to the view.

# Structure #
<img src='http://s4.postimg.org/4gg5e4r59/mvvm_structure.png' width='600px' /><br />
The ModelViewViewModel design pattern is made up of three components it has a view a viewModel and a model. We will discuss each of these components in more detail later on but for now I want to concentrate on the structure of the pattern.
Lets begin with the view, The view may or may not have a reference to the viewModel it really depends on how you implement this pattern in your applications. When I say that the view does not have a reference to the viewModel I mean that there will not be a hard coded instance created either in the view XAML or in the code behind. There will be an instance, if you will of the viewModel in its DataContext but that could be set by other mechanisms such as a factory pattern of some type. But at any case the view will never know about the model it should never have a reference to the model. The only component in the MVVM pattern that has a reference to the model will be the viewModel. The ViewModel may or may not have a reference to the view. Once again it just really depends on how you decide to implement this pattern in your application. If you decide to have the viewModel create the view which is the viewModel first approach, then the viewModel will have a reference to the view, if you decide to go view first approach then the view would have a reference to the viewModel. But once again if you are using a factory pattern it is completely possible that neither one of them have any reference to each other. The model is completely oblivious to the fact that the viewModel and the view even exist. This is a very loosely coupled design which pays dividends in many ways.

# Demo: Creating the Application #
Lets create a simple MVVM application. Lets start off by creating a new Silverlight application and call it SimpleMvvm, we will not need to create a web site to host this demo application. Now lets create three folders "Models", "ViewModels" and "Views". Now lets move the MainPage to the Views folder couse we are going to use that as our main view. Now lets create a model object class and name it "Person" and we are also going to need a viewModel so lets create a class and name it MainViewModel. Now put the Person class into the Models folder and the MainviewModel class to the ViewModels folder. And that is how this project structure should look like at this point.<br />
<img src='http://s24.postimg.org/n1ylgwtut/project_structure.png' />
<br />

# The Model #
<img src='http://s13.postimg.org/ubanyv9t3/the_model.png' width='800px' />
<br />
The model is the data and you can look at the model from two different perspectives, there is the object oriented approach where your model may be represented by domain specific objects, or you can use the data centric approach where your model may be a XML file or a data access layer or anything else that is responsible for communicating with the data store. Either way the models purpose is to represent the data points and it has no knowledge of where it will be presented or how it will be presented. It's singel responsibility is to represent the data points. On the image we have a person object with two properties, this person object implements two interfaces the INotifyPropertyChanged interface which is responsible for notifying the UI or presentation layer of any changes made to a properties value, the IDataErrorInfo interface is used to notify the UI if a validation role has failed against one of those properties.

# Demo: Creating the Model #
We start by giving our person model object some properties that we are going to be binding our UI to. Person normally has a first name, and a last name and at some point we are going to update our person so lets give it a DateTime property named UpdatedDate. In order to let my UI know that any of these property values change we are going to have to implement an interface INotifyPropertyChanged. The implementation is simple we are going to have a public method that takes a string parameter.
```
public event PropertyChangedEventHandler PropertyChanged;
public void OnPropertyChanged(string property)
{
  if(PropertyChanged != null)
    PropertyChanged(this, new PropertyChangedEventArgs(property));
}
```
Now that we have implemented the interface we need to modify our properties so when they do change we can let them know.
```
private string _FirstName;
public string FirstName
{
  get { return _FirstName; }
  set
  {
    _FirstName = value;
    
    //Add this code to the properties so we can know when the property changes
    OnPropertyChanged("FirstName");
  }
}
```
Now our properties are firing the PropertyChangedEvent when the value changes. This allows the view to display any changes that have been made to the underlying objects properties. Now we got to worry about something else, we got to worry about validation, how do I send validation errors up to my view? This is accomplished by implementing the IDataErrorInfo interface. For the Error property we are going to return null. This is the objects level error, so we would set this for the objects level validation.
```
public string Error
{
  get { return null; }
}
```
We are going to keep the validation on our Person object pretty simple, so we are just going to validate that the first name has been entered.
```
public string this{string columnName]
{
  get
  {
    string error = string .Empty;

    if(string.isNullOrEmpty(_FirstName))
      error = "First Name is required";

    return error;
  }
}
```
Now anytime this validation error fails my UI will be notified thru the IDataErrorInfo interface and send a notification to the user.

**Person class**
```
public class Person : INotifyPropertyChanged, IDataErrorInfo 
{
  private string _FirstName
  public string FirstName
  {
    get { return _FirstName; }
    set
    {
      _FirstName = value;
      OnPropertyChanged("FirstName");
    }
  }

  private string _LastName
  public string LastName
  {
    get { return _LastName; }
    set
    {
      _LastName = value;
      OnPropertyChanged("LastName");
    }
  }

  private DateTime _UpdateDate;
  public DateTime UpdateDate
  {
    get { return _UpdateDate; }
    set
    {
      _UpdateDate = value;
      OnPropertyChanged("UpdateDate");
    }
  }

  public event PropertyChangedEventHandler PropertyChanged;
  public void OnPropertyChanged(string property)
  {
    if(PropertyChanged != null)
      PropertyChanged(this, new PropertyChangedEventArgs(property));
  }

  public string Error
  {
    get { return null; }
  }
  public string this{string columnName]
  {
    get
    {
      string error = string .Empty;

      if(string.isNullOrEmpty(_FirstName))
        error = "First Name is required";

      return error;
    }
  }
}
```

# The ViewModel #

<img src='http://s14.postimg.org/rsph57ru9/View_Model.png' width='800px' />
<br />
You may be wondering what exactly goes into a viewModel if my data and its data points are in the model, but the viewModel contains properties commands and other abstractions to facilitate communication between the view and the model. What does this mean? Well it means that the viewModel will have properties that expose instances of your model objects it will also have commands or events that interact with the view to respond to any type of user interaction at a minimum you want your viewModel to implement the INotifyPropertyChanged interface. this is because your view will be bound to your viewModel and the INotifyPropertyChanged interface is the mechanism which pushes data up to the view. As you can see we have a viewModel called MainViewModel, it exposes a property of type Person, and this is the Person domain object that was in our view slide. We are exposing an instance of that Person object to our view. And any time the Person object instance changes the view will be notified of that change and update accordingly. There has been some debate recently about what your viewModel should inherit from. A lot of people have their viewModels inherit from DependencyObject or UI element. Although the viewModel manages the state and behaviour of the view it should not have dependency on a UI base object, basicly you do not want any control related objects in your viewModel.

# Demo: Creating the ViewModel #
Now lets work on our viewModel and open up the MainViewModel class, at minimum my viewModel has to implement the INotifyrPropertyChanged interface.
```
public event PropertyChangedEventHandler PropertyChanged;
public void OnPropertyChanged(string propertyName)
{
  if(PropertyChanged != null)
    PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
}
```
Now we have our INotifyPropertyChanged interface implemented and we have to have this at minimum because this is the mechanism that pushes all the changes and our base data up to the view. The next thing I want to do is I want to expose a property that is an instance of my model object.
```
private Person _ModelPerson;
public Person ModelPerson
{
  get { return _ModelPerson; }
  set
  {
    _ModelPerson = value;
    OnPropertyChanged("ModelPerson");
  }
}
```
And we need to add that OnPropertyChanged to that property so whenever that property changes it updates the view. And now we need to populate this Person object with an instance of something, so lets first create a constructor for our viewModel and create our Person instance in the constructor so when we create a new instance of our viewModel we create and assign a new instance of our Person object to our ModelPerson property.
```
public MainViewModel()
{
  LoadPerson();
}

private void LoadPerson()
{
  ModelPerson = new Person()
  {
    FirstName = "Brian",
    LastName = "Lagunas"
  };
}
```

# The View #
<img src='http://s23.postimg.org/vjgpkhijf/The_view.png' width='800px' />
<br />
The view is simply the visual display, it consists of all the elements displayed by the user interface such as buttons and windows and graphics and all the other controls that we know and love. The view is responsible for displaying data and collecting data from users. It is not responsible for retrieving data, business logic, business rules or validation. In WPF and Silverlight the view contains binding extensions that identify the data points that will be represented to the user. The bindings point to the names of the data point properties. But they don't have awareness of where those properties are or where they come from. The bindings are activated when the views dataContext is set to a class that contains the source for the bindings. It is that data binding infrastructure that is build in to WPF and Silverlight that allows designers to create rich and very interactive user interfaces without ever having to know the details or specifics of where the data points come from.

# Demo: Creating the View #
Now that we have our viewModel finished up lets go ahead and start our view.

Here we have a basic view to display our data points, and that is where we put on our designer hat and I have no idea what the programmers are doing, all I have to focus on is creating a rich UI that the users are going to interact with, but I do know the data points that I'm expecting I know the First Name text box is designated for a FirstName property, so I'm going to set my text to bind to the ModelPerson.FirstName, and I'm gonna do that for the other properties to. I'm also aware that the firstName has validation rules applied to it. So to enable those I'm going to set the ValidatesOnDataErrors=True. Now the designers part is finished and time for the programmers turn. And now he is going to hook up the viewModel to this view. We need to begin by adding a namespace, this namespace is the location where our viewModel exists ` xmlns:local="Clr-namespace:SimpleMvvm" ` for demonstration purpose we are going to create the viewModel as a resource and use the static resource data binding syntax to set the datacontext of the view. We need to give it a Key that is how we reference it. Now we have to set the dataContext to that resource
```
<UserControl.Resources>
  <local:MainViewModel x:key="MainViewModel" />
</UserControl.Resources>
```
```
<Grid x:Name="LayoutRoot" Margin="50" DataContext="{StaticResource MainViewModel}" >
```
And that should do it, we should see the data if we run the program. You can now notice that in the designer you can actually see the data that I was going to load at runtime, if you remember early in the module we discussed a concept called blendability, the ability to see design time data. So by using the XAML approach for creating my viewModel I have actually enabled design time data, what is happening is my view is creating an instance of that viewModel at design time and it is newing up that object for me so I can see the underlying data as it exists now. In Silverlight you have to specify the mode in which your data binding will behave, TwoWay meaning that any changes that I make from the UI perspective will be pushed down to the underlying data model as well as being pushed up.
```
Text="{Binding ModelPerson.LastName, Mode=TwoWay}"
```

And here is how the view looks:
```
<UserControl x:Class="SimpleMvvm.MainPage"
	     xmlns="http://schemas.microsoft.com/winfx/2006/xaml/persentation"
	     xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
	     xmlns:d="http://schemas.microsoft.com/expression/blend/2008"
	     xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006"
	     mc:Ignorable="d"
	     d:DesignHeight="300" d:DesignWidth="400">
  <Grid x:Name="LayoutRoot" Margin="50">
    <Grid.ColumnDefinitions>
      <ColumnDefinition Width="Auto />
      <ColumnDefinition />
    </Grid.ColumnDefinitions>
    <Grid.RowDefinitions>
      <RowDefinition Height="Auto"/>
      <RowDefinition Height="Auto"/>
      <RowDefinition Height="Auto"/>
      <RowDefinition Height="Auto"/>
    </Grid.RowDefinitions>

    <!-- First Name -->
    <TextBlock text="First Name:" Margin="5" />
    <TextBox Grid.Column="1" Margin="5" Text="{Binding ModelPerson.FirstName, Mode=TwoWay, ValidatesOnDataErrors=True}" />

    <!-- Last Name -->
    <TextBlock Grid.Row="1" Text="Last Name:" Margin="5" />
    <TextBox Grid.Row="1" Grid.Column="1" Margin="5" Text="{Binding ModelPerson.LastName, Mode=TwoWay}" />

    <!-- Last Updated -->
    <TextBlock Grid.Row="2" Text="Last Updated:" Margin="5" />
    <textBox Grid.Row="2" Grid.Column="1" Margin="5" Text="{Binding ModelPerson.UpdatedDate}" />
  </Grid>
</UserControl>
```

# Binding the View and ViewModel #
I mentioned earlier that to create the connection between the view and the viewModel all you have to do is set the dataContext of the view to an instance of a viewModel object. Well establishing that connection can be accomplished in a variety of ways.
  * Declaratively in XAML - this is known as the view first approach, meaning that the view is responsible for instantiating the viewModel. Using this method you create an instance of your viewModel as a resource, and use the static resource binding syntax to set the dataContext of your view. There are a couple of drawbacks of using this approach, one you can not control the instantiation of your viewModel, and two you can not pass parameters to an overloaded constructor. Because of these limitations I find it difficult to use this approach when building anything except a hello world demo application.
  * Imperatively in Code - in this method the viewModel is created and assigned to the dataContext of the view in the views code behind. Normally in the constructor of the view after the Initialize method, or in the views Loaded event, using this approach you can now control the instantiation of your viewModel and you can also pass parameters to an overloaded constructor. Now some of the MVVM purists out there may argue with you that you now have code in your code behind and it should not be there, my response to that is if you are comfortable with code in your view well than that is perfectly fine, remember my view is that patterns are just a starting point and they are not gospel on how you have to follow everything to the tee or your application is not going to work.
  * ViewModel Locator - this is actually pretty popular in a lot of the MVVM frameworks that are out there. Basically this is the service locator pattern renamed for MVVM purposes, the basic idea of the ViewModel Locator method is that you have a class that contains references to all the viewModels in your application, and it encapsulates the logic to locate and create them and you can intermix that with IoC and things like that
  * Data Templates
  * Inversion of Control (IoC)
  * Factory Pattern with Inversion of Control - you can have the factory responsible for creating an instance of your viewModel, a instance of your view and then setting the dataContext in there and you can use Inversion of Control to create thous instances for you.
  * and more... -  I guess what I'm trying to say is there are many many ways to create the binding between the view and the viewModel, you should just use your favorite search engine to try and find the approach that fits your programming style and your applications needs.

# Communication Between the View and ViewModel #
As mentioned before communication between the view and the viewModel can be accomplished in a couple of different ways. The two most common approaches are by using events and commands. Commands are similar to events except you can associate any number of UI controls or input gestures to a command, and binde that command to a handler that is executed when the controls are activated or gestures are performed. commands also keep track of whether or not they are available, if a command is disabled all UI elements associated with that command are disabled as well. The code that executes when the command is invoked will be located in the commands Execute event handler. The code that determines if the command can or can not be invoked will be located in the commands CanExecute event handler
  * vents
  * Commands
    * Execute
    * CanExecute

# Demo: Communicating with Commands #
I'm gonna put my designer hat on one more time and say that I have another requirement I just got in, so I take my view and I have just learned that this button is going to execute a command, and that command is going to exist in my viewModel and it is going to be called SavePersonCommand. So right now if I were to run this and I do not add that command to my ViewModel, we would expect a binding expression error, and that is exactly what we get. And that is letting me know that the button is looking at the viewModel for a command called SavePersonCommand. Lets go ahead and open up our viewModel, instead of creating another class we are just going to stick it at the bottom of the viewModel class. It needs to implement the ICommand interface, we are going to need an action, this action is gonna be what executes when the button is clicked, we'll just call that executeMethod. the CanExtecute tells me if I'm even allowed to execute this command yes or no, true or false. For demo purposes we are just going to say anyone can update this command, if it was false the button would be grayed out and you would not be able to click the button. Now I'm going to take my Execute method and invoke it. I've created a simple command, but now what I have to do is I have to create a property in my viewModel that exposes that command, and then we have to create an instance of the SavePersonCommand, so lets create a method called InitializeCommand. The UpdatePerson method is going to be the action or method that is invoked when a command is executed. Now we need to create a constructor in our SavePersonCommand class that takes in Action parameter.

```
<!-- Save Button -->
<Button Grid.Row="3" Grid.ColumnSpan="2" Content="Save" Margin="10" Command="{Binding SavePersonCommand}" />

public class SavePersonCommand : ICommand
{
  public SavePersonCommand(Action updatePerson)
  {
    _executeMethod = updatePerson;
  }

  Action _executeMethod;


  public bool CanExecute(object parameter)
  {

  }

  public event EventHandler CanExecuteChanged;
  public void Execute(object parameter)
  {
    _executeMethod.Invoke();
  }
}

//In the viewModel
private ICommand _SavePersonCommand;
public ICommand SavePersonCommand
{
  get { return SavePersonCommand; }
  set 
  {
    _SavePersonCommand = value; 
    OnPropertyChanged("SavePersonCommand");
  }
}

public MainViewModel()
{
  InitializeCommand();
  LoadPerson();
}

public void InitializeComman()
{
  SavePersonCommand = new SavePersonCommand(UpdatePerson);
}

private void UpdatePerson()
{
  ModelPerson.UpdatedDate = Datetime.Now;
}
```

# Collaboration #
This may be a little bit of review, but now that we have an understanding of each of the different components of the ModelViewViewModel triad, lets discuss a little bit of how these different components collaborate with one another. The viewModel is responsible for the state and behaviour of the view and it acts as a facade to the underlying model think of the viewModel as the middleman between the view and the model. It is responsible for sending model information to the view and view information back to the model. The viewModel is nothing more than abstraction of the view's code behind, except that the viewModel has no hard dependency on any view. This makes it possible to reuse a viewModel across multiple views and even multiple platforms. The view binds to properties on a viewModel which in turn exposes data contained in model objects and other state that are specific to the view. The binding between the view and viewModel are simple to construct because all you have to do is set the dataContext of a view to an instance of a viewModel object. I want to reiterate that your viewModel must implement the INotifyPropertyChanged interface, because if property values change in the viewModel those new values will automatically propagate it to the view via data binding. And that will only happen if your viewModel implements the INotifyPropertyChanged interface. When the user click a button in the view a command on the viewModel executes to perform the requested action. It is the viewModel, never the view that performs all modifications made to the model.

<img src='http://s21.postimg.org/ydc051fyv/collaboration.png' width='800px' />

# Consequences #
  * **Pro**
    * Reduce code-behind
    * Model doesn't need to change to suppoert a view
    * Designers design, coders code
    * Reduces development time
    * Multi-targeting (project linking)
  * **Con**
    * Create more files
    * Simple tasks can be complicated
    * Lack of standardization
    * Specific to WPF and Silverlight platforms
So what are some of the consequences we see when we use the ModelViewViewModle design pattern. For one, we reduce code-behind by abstracting the view out into a viewModel, we eliminate most, if not all of the view's code-behind. The model does not need to change to support a view, we can create a new viewModel or modify an existing one to present data points to the user differently depending on application requirements. Designers get to design, coders get to code, this supports simultaneous development, while designers are creating views and rich user interfaces, the programmers are writing the core code and business logic, this also reduces development time because the work is being done simultaneously. Of course using the ModelViewViewModel pattern you have to create more files. This bothers some people more than others. To me I think its a great way to help keep my project organised and structured well. Simple tasks can be complicated to perform, such as setting focus to an element in the view, there is a lack of standardisation, but I really don't see this as a con but I listed it as one due to the feedback I have received from the WPF/Silverlight community. My view is that the MVVM pattern is just that it is a pattern. It is just a set of guidelines to help facilitate abstraction to my applications. You could almost consider this a positive, since the pattern is flexible enough to meet anyone's needs and can be implemented in so many ways. And once again I mention this a few times but it is specific to the WPF and Silverlight platforms. I guess if you were not using this pattern in a WPF or a Silverlight platform you would be calling it the presentation model.

# Known Uses #
  * Microsoft
  * UFC Gym
  * US Army
  * Family.Show
  * PRISM Reference Implementation
  * AQUA
  * Many frameworks
    * MVVM Light Toolkit
    * Caliburn
    * Cinch
    * Onyx
    * MVVM Foundation
    * And more...
If you are interested in who is actually using this pattern, just to name a few.

# Related Patterns #
  * Model View Presenter (MVP)
  * Model View Controller (MVC)
  * Presentation Model (PM)
Here are some of the related patterns to the ModelViewViewModel design pattern. These patterns like the MVVM pattern are considered view separation design pattern. We have the Model view Presenter, the Model view Controller, both of which we will have modules in our ondemand library so you can learn more about those. And then I've already mentioned and described a little bit about the Presentation Model, for more information on that you can go to Martin Fowler website or search on you favorite search engine.