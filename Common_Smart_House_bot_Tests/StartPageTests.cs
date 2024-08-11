//using Common_Smart_House_bot;
//using Common_Smart_House_bot.User;
//using Common_Smart_House_bot.User.Pages;
//using Common_Smart_House_bot.User.Pages.PageResult;
//using Telegram.Bot.Types;
//using Telegram.Bot.Types.ReplyMarkups;

//namespace Common_Smart_House_bot_Tests
//{
//    public class StartPageTests
//    {
//        [Test]
//        public void View_FirstEnter_CorrectTextAndKeyboard()
//        {
//            // Arrange
//            var startPage = new StartPage();
//            var pages = new Stack<IPage>([new NotStatedPage()]);
//            var userState = new UserState(pages, new UserData());
//            var expectedButtons = new InlineKeyboardButton[][]
//            {
//                [InlineKeyboardButton.WithCallbackData("Управление умным домом")],
//                [InlineKeyboardButton.WithCallbackData("Просмотр всех событий")],
//                [InlineKeyboardButton.WithCallbackData("Настройка оповещений")]
//            };

//            // Act
//            var result = startPage.View(null, userState);

//            // Assert
//            Assert.That(result.GetType(), Is.EqualTo(typeof(PhotoPageResult)));
//            Assert.IsInstanceOf<InlineKeyboardMarkup>(result.ReplyMarkup);
//            Assert.That(result.UpdatedUserState.CurrentPage, Is.EqualTo(startPage));
//            Assert.That(result.UpdatedUserState.Pages.Count, Is.EqualTo(2));
//            Assert.That(result.Text, Is.EqualTo(Resources.StartPageText));
//            KeyBoardHelper.AssertKeyboard(expectedButtons, (InlineKeyboardMarkup)result.ReplyMarkup);
//        }

//        [Test]
//        public void Handle_SmartHomeManagementCallback()
//        {
//            // Arrange
//            var startPage = new StartPage();
//            var pages = new Stack<IPage>([new NotStatedPage(), startPage]);
//            var userState = new UserState(pages, new UserData());
//            var update = new Update() { CallbackQuery = new CallbackQuery() { Data = "Управление умным домом" } };

//            // Act
//            var result = startPage.Handle(update, userState);

//            // Assert
//            Assert.That(result.GetType(), Is.EqualTo(typeof(VideoPageResult)));
//            Assert.IsInstanceOf<SmartHomeManagementPage>(result.UpdatedUserState.CurrentPage);
//            Assert.That(result.UpdatedUserState.Pages.Count, Is.EqualTo(3));
//        }

//        [Test]
//        public void Handle_LastEventsCallback()
//        {
//            // Arrange
//            var startPage = new StartPage();
//            var pages = new Stack<IPage>([new NotStatedPage(), startPage]);
//            var userState = new UserState(pages, new UserData());
//            var update = new Update() { CallbackQuery = new CallbackQuery() { Data = "Просмотр всех событий" } };

//            // Act
//            var result = startPage.Handle(update, userState);

//            // Assert
//            Assert.That(result.GetType(), Is.EqualTo(typeof(PageResultBase)));
//            Assert.IsInstanceOf<LastEventsPage>(result.UpdatedUserState.CurrentPage);
//            Assert.That(result.UpdatedUserState.Pages.Count, Is.EqualTo(3));
//        }

//        [Test]
//        public void Handle_SettingUpAlertsCallback()
//        {
//            // Arrange
//            var startPage = new StartPage();
//            var pages = new Stack<IPage>([new NotStatedPage(), startPage]);
//            var userState = new UserState(pages, new UserData());
//            var update = new Update() { CallbackQuery = new CallbackQuery() { Data = "Настройка оповещений" } };

//            // Act
//            var result = startPage.Handle(update, userState);

//            // Assert
//            Assert.That(result.GetType(), Is.EqualTo(typeof(AudioPageResult)));
//            Assert.IsInstanceOf<SettingUpAlertsPage>(result.UpdatedUserState.CurrentPage);
//            Assert.That(result.UpdatedUserState.Pages.Count, Is.EqualTo(3));
//        }

//        [Test]
//        public void Handle_UnknowMessage_StartPageView()
//        {
//            // Arrange
//            var startPage = new StartPage();
//            var pages = new Stack<IPage>([new NotStatedPage(), startPage]);
//            var userState = new UserState(pages, new UserData());
//            var update = new Update() { Message = new Telegram.Bot.Types.Message() { Text = "Неверное сообщение" } };
//            var expectedButtons = new InlineKeyboardButton[][]
//           {
//                [InlineKeyboardButton.WithCallbackData("Управление умным домом")],
//                [InlineKeyboardButton.WithCallbackData("Просмотр всех событий")],
//                [InlineKeyboardButton.WithCallbackData("Настройка оповещений")]
//           };

//            // Act
//            var result = startPage.Handle(update, userState);

//            // Assert
//            Assert.That(result.GetType(), Is.EqualTo(typeof(PhotoPageResult)));
//            Assert.IsInstanceOf<InlineKeyboardMarkup>(result.ReplyMarkup);
//            Assert.That(result.UpdatedUserState.CurrentPage, Is.EqualTo(startPage));
//            Assert.That(result.UpdatedUserState.Pages.Count, Is.EqualTo(2));
//            Assert.That(result.Text, Is.EqualTo(Resources.StartPageText));
//            KeyBoardHelper.AssertKeyboard(expectedButtons, (InlineKeyboardMarkup)result.ReplyMarkup);
//        }
//    }
//}