using System;
using BDD_Demo.Domain.Contexts;
using BDD_Demo.Services;
using BDD_Demo.Services.Requests;
using BDD_Demo.Services.ViewModels;
using FluentAssertions;
using TechTalk.SpecFlow;

namespace SpecFlowProject.Steps
{
    [Binding]
    public class TodosNotificationsTests
    {
        private InMemoryBddDemoContext _context;
        private UserService _userService;
        private TodoService _todoService;
        private UserViewModel _user;
        private TodoViewModel _todo;

        public TodosNotificationsTests()
        {
            _context = new InMemoryBddDemoContext();
            _userService = new UserService(_context);
            _todoService = new TodoService(_context, _userService);
        }


        [When(@"a todo is added to the user with the target date of (.*)")]
        public async void WhenATodoIsAddedToTheUserWithTheTargetDateOfX(string date)
        {
            _todo = await _todoService.AddTodo(new AddTodoRequest()
                {UserId = _user.Id, Title = "test todo", TargetDate = DateTime.Parse(date), Text = "test todo body"});
        }


        [When(@"the todo is set to complete")]
        public void WhenTheTodoIsSetToComplete()
        {
            _todoService.CompleteTodo(new CompleteTodoRequest() {Id = _todo.Id});
        }

        [Given(@"a new user is added to the database with the username (.*) and the email (.*)")]
        public async void GivenANewUserIsAddedToTheDatabaseWithTheUsernameAndEmail(string username, string email)
        {
            _user = await _userService.AddUser(new AddUserRequest() {Email = email, UserName = username});
        }

        [Then(@"if the user logs in he will have (.*) notifications")]
        public async void ThenIfTheUserLogsInHeWillHave0Notifications(string numberOfNotifications)
        {
            // First, get User again
            var user = await _userService.GetUser(_user.Id);
            user.Notifications.Count.Should().Be(int.Parse(numberOfNotifications));
        }

        [When(@"I have (.*) new notifications")]
        public async void GivenIHaveANotification(string numberOfNotifications)
        {
            var _todo = await _todoService.AddTodo(new AddTodoRequest()
                {UserId = _user.Id, Title = "test todo", TargetDate = DateTime.Now, Text = "test todo body"});
            var todo2 = await _todoService.AddTodo(new AddTodoRequest()
                {UserId = _user.Id, Title = "test todo", TargetDate = DateTime.Now, Text = "test todo body"});
        }

        [Then(@"I expect to be able to see (.*) notifications")]
        public async void ThenIExpectToBeAbleToSeeIt(string numberOfNotifications)
        {
            var user = await _userService.GetUser(_user.Id);
            user.Notifications.Count.Should()
                             .Be(int.Parse(numberOfNotifications));
        }
    }
}