# Практическая работа №6 (часть 3) 
# СОЗДАНИЕ АВТОМАТИЗИРОВАННЫХ UNIT-ТЕСТОВ

## Выполнили
- Васильев Кирилл
- Погуляев Андрей
Группа 3ИСИП-123

## Результаты тестов
<img width="821" height="320" alt="image" src="https://github.com/user-attachments/assets/f729cf82-2731-4fa5-a9ba-aa9ca61c43d9" />

## Таблица Users
<img width="583" height="134" alt="image" src="https://github.com/user-attachments/assets/7e61c590-b9b8-4ac6-ad4d-2de715cd8fce" />
Все тесты пройдены успешно.

### Авторизация (`Cinema.AuthorizationTests`)
- `AuthTestSuccess` – вход всех пользователей из БД  
- `AuthTestFail` – негативные сценарии (пустые поля, неверный пароль, несуществующий логин) 

### Регистрация (`Cinema.RegistrationTest`)
- `RegisterTestSuccess` – создание нового пользователя   
- `RegisterTestFail` – пустые поля, дублирование email  

## Вывод
Тесты успешно выполнены, так как:
- Методы `Auth` и `Register` корректно обрабатывают валидные/невалидные данные.
- Обработаны все негативные сценарии (пустые поля, дублирование email, неверный пароль).
- Подключение к БД настроено через `App.config`, тесты используют те же данные, что и приложение.


## Скрипт 
```sql
USE [cinema]
GO
/****** Object:  Table [dbo].[Genres]    Script Date: 29.03.2026 11:33:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Genres](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Halls]    Script Date: 29.03.2026 11:33:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Halls](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Capacity] [int] NOT NULL,
	[RatingQuality] [nvarchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MovieGenres]    Script Date: 29.03.2026 11:33:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MovieGenres](
	[MovieId] [int] NOT NULL,
	[GenreId] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[MovieId] ASC,
	[GenreId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Movies]    Script Date: 29.03.2026 11:33:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Movies](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](200) NOT NULL,
	[Description] [nvarchar](1000) NULL,
	[PosterPath] [nvarchar](500) NULL,
	[Rating] [decimal](3, 1) NULL,
	[ReleaseDate] [date] NOT NULL,
	[AgeRating] [nvarchar](10) NOT NULL,
	[DurationMinutes] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Seats]    Script Date: 29.03.2026 11:33:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Seats](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[HallId] [int] NOT NULL,
	[RowNumber] [int] NOT NULL,
	[SeatNumber] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Sessions]    Script Date: 29.03.2026 11:33:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Sessions](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[MovieId] [int] NOT NULL,
	[HallId] [int] NOT NULL,
	[DateTime] [datetime] NOT NULL,
	[Price] [decimal](10, 2) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tickets]    Script Date: 29.03.2026 11:33:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tickets](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[SessionId] [int] NOT NULL,
	[UserId] [int] NOT NULL,
	[SeatId] [int] NOT NULL,
	[PurchaseDate] [datetime] NULL,
	[PricePaid] [decimal](10, 2) NOT NULL,
	[Status] [nvarchar](20) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 29.03.2026 11:33:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Email] [nvarchar](100) NOT NULL,
	[Password] [nvarchar](100) NOT NULL,
	[FullName] [nvarchar](100) NOT NULL,
	[BirthDate] [date] NULL,
	[CreatedAt] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Genres] ON 

INSERT [dbo].[Genres] ([Id], [Name]) VALUES (1, N'Боевик')
INSERT [dbo].[Genres] ([Id], [Name]) VALUES (8, N'Детектив')
INSERT [dbo].[Genres] ([Id], [Name]) VALUES (2, N'Драма')
INSERT [dbo].[Genres] ([Id], [Name]) VALUES (3, N'Комедия')
INSERT [dbo].[Genres] ([Id], [Name]) VALUES (9, N'Мелодрама')
INSERT [dbo].[Genres] ([Id], [Name]) VALUES (7, N'Мультфильм')
INSERT [dbo].[Genres] ([Id], [Name]) VALUES (10, N'Приключения')
INSERT [dbo].[Genres] ([Id], [Name]) VALUES (5, N'Триллер')
INSERT [dbo].[Genres] ([Id], [Name]) VALUES (6, N'Ужасы')
INSERT [dbo].[Genres] ([Id], [Name]) VALUES (4, N'Фантастика')
SET IDENTITY_INSERT [dbo].[Genres] OFF
GO
SET IDENTITY_INSERT [dbo].[Halls] ON 

INSERT [dbo].[Halls] ([Id], [Name], [Capacity], [RatingQuality]) VALUES (1, N'Зал 1 (2D)', 100, N'2D')
INSERT [dbo].[Halls] ([Id], [Name], [Capacity], [RatingQuality]) VALUES (2, N'Зал 2 (3D)', 80, N'3D')
INSERT [dbo].[Halls] ([Id], [Name], [Capacity], [RatingQuality]) VALUES (3, N'Зал 3 (IMAX)', 120, N'IMAX')
INSERT [dbo].[Halls] ([Id], [Name], [Capacity], [RatingQuality]) VALUES (4, N'Зал 4 (VIP)', 50, N'Dolby Atmos')
INSERT [dbo].[Halls] ([Id], [Name], [Capacity], [RatingQuality]) VALUES (5, N'Зал 5 (Стандарт)', 90, N'2D')
SET IDENTITY_INSERT [dbo].[Halls] OFF
GO
INSERT [dbo].[MovieGenres] ([MovieId], [GenreId]) VALUES (1, 5)
INSERT [dbo].[MovieGenres] ([MovieId], [GenreId]) VALUES (1, 8)
INSERT [dbo].[MovieGenres] ([MovieId], [GenreId]) VALUES (2, 1)
INSERT [dbo].[MovieGenres] ([MovieId], [GenreId]) VALUES (2, 4)
INSERT [dbo].[MovieGenres] ([MovieId], [GenreId]) VALUES (2, 10)
INSERT [dbo].[MovieGenres] ([MovieId], [GenreId]) VALUES (3, 3)
INSERT [dbo].[MovieGenres] ([MovieId], [GenreId]) VALUES (3, 7)
INSERT [dbo].[MovieGenres] ([MovieId], [GenreId]) VALUES (3, 8)
INSERT [dbo].[MovieGenres] ([MovieId], [GenreId]) VALUES (3, 10)
INSERT [dbo].[MovieGenres] ([MovieId], [GenreId]) VALUES (4, 2)
INSERT [dbo].[MovieGenres] ([MovieId], [GenreId]) VALUES (4, 3)
INSERT [dbo].[MovieGenres] ([MovieId], [GenreId]) VALUES (4, 7)
INSERT [dbo].[MovieGenres] ([MovieId], [GenreId]) VALUES (4, 10)
INSERT [dbo].[MovieGenres] ([MovieId], [GenreId]) VALUES (5, 1)
INSERT [dbo].[MovieGenres] ([MovieId], [GenreId]) VALUES (5, 5)
INSERT [dbo].[MovieGenres] ([MovieId], [GenreId]) VALUES (5, 8)
INSERT [dbo].[MovieGenres] ([MovieId], [GenreId]) VALUES (6, 5)
INSERT [dbo].[MovieGenres] ([MovieId], [GenreId]) VALUES (6, 6)
GO
SET IDENTITY_INSERT [dbo].[Movies] ON 

INSERT [dbo].[Movies] ([Id], [Title], [Description], [PosterPath], [Rating], [ReleaseDate], [AgeRating], [DurationMinutes]) VALUES (1, N'Горничная', N'Милли мечтает начать жизнь с чистого листа и с радостью принимает работу горничной в роскошном особняке семьи Винчестер. Но за закрытыми дверями и странными правилами скрывается нечто зловещее.', N'gornicha.png', CAST(7.8 AS Decimal(3, 1)), CAST(N'2025-01-15' AS Date), N'18+', 128)
INSERT [dbo].[Movies] ([Id], [Title], [Description], [PosterPath], [Rating], [ReleaseDate], [AgeRating], [DurationMinutes]) VALUES (2, N'Аватар 3', N'Джейк Салли, Нейтири и их дети переживают смерть Нетейама. Противостояние с корпорацией RDA обостряется, и теперь семье предстоит столкнуться с враждебным племенем На`ви во главе с Варанг.', N'avatar.png', CAST(8.9 AS Decimal(3, 1)), CAST(N'2025-12-19' AS Date), N'12+', 192)
INSERT [dbo].[Movies] ([Id], [Title], [Description], [PosterPath], [Rating], [ReleaseDate], [AgeRating], [DurationMinutes]) VALUES (3, N'Зверополис 2', N'Джуди Хоппс и Ник Уайлд возвращаются в новом захватывающем приключении! В Зверополисе появляется таинственный преступник, использующий новые технологии.', N'zver.png', CAST(8.5 AS Decimal(3, 1)), CAST(N'2025-11-26' AS Date), N'0+', 110)
INSERT [dbo].[Movies] ([Id], [Title], [Description], [PosterPath], [Rating], [ReleaseDate], [AgeRating], [DurationMinutes]) VALUES (4, N'Чебурашка 2', N'Продолжение истории самого любимого пушистого героя! Чебурашка и Гена встречают новых друзей и отправляются в увлекательное путешествие.', N'chebur.png', CAST(8.2 AS Decimal(3, 1)), CAST(N'2025-12-05' AS Date), N'0+', 115)
INSERT [dbo].[Movies] ([Id], [Title], [Description], [PosterPath], [Rating], [ReleaseDate], [AgeRating], [DurationMinutes]) VALUES (5, N'Иллюзия обмана 3', N'Четверка иллюзионистов возвращается для своего самого рискованного трюка. Новый враг, новые иллюзии и невероятные разоблачения.', N'obman.png', CAST(7.9 AS Decimal(3, 1)), CAST(N'2025-08-22' AS Date), N'12+', 135)
INSERT [dbo].[Movies] ([Id], [Title], [Description], [PosterPath], [Rating], [ReleaseDate], [AgeRating], [DurationMinutes]) VALUES (6, N'Пункт назначения: Новый аттракцион', N'Группа друзей посещает новый экстремальный аттракцион в парке развлечений. После череды странных совпадений они понимают, что Смерть снова идет по их следам.', N'death.png', CAST(7.4 AS Decimal(3, 1)), CAST(N'2025-10-31' AS Date), N'18+', 85)
SET IDENTITY_INSERT [dbo].[Movies] OFF
GO
SET IDENTITY_INSERT [dbo].[Seats] ON 

INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (1, 1, 1, 1)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (2, 1, 2, 1)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (3, 1, 3, 1)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (4, 1, 4, 1)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (5, 1, 5, 1)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (6, 1, 6, 1)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (7, 1, 7, 1)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (8, 1, 8, 1)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (9, 1, 9, 1)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (10, 1, 10, 1)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (11, 1, 1, 2)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (12, 1, 2, 2)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (13, 1, 3, 2)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (14, 1, 4, 2)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (15, 1, 5, 2)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (16, 1, 6, 2)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (17, 1, 7, 2)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (18, 1, 8, 2)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (19, 1, 9, 2)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (20, 1, 10, 2)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (21, 1, 1, 3)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (22, 1, 2, 3)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (23, 1, 3, 3)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (24, 1, 4, 3)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (25, 1, 5, 3)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (26, 1, 6, 3)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (27, 1, 7, 3)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (28, 1, 8, 3)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (29, 1, 9, 3)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (30, 1, 10, 3)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (31, 1, 1, 4)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (32, 1, 2, 4)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (33, 1, 3, 4)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (34, 1, 4, 4)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (35, 1, 5, 4)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (36, 1, 6, 4)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (37, 1, 7, 4)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (38, 1, 8, 4)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (39, 1, 9, 4)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (40, 1, 10, 4)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (41, 1, 1, 5)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (42, 1, 2, 5)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (43, 1, 3, 5)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (44, 1, 4, 5)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (45, 1, 5, 5)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (46, 1, 6, 5)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (47, 1, 7, 5)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (48, 1, 8, 5)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (49, 1, 9, 5)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (50, 1, 10, 5)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (51, 1, 1, 6)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (52, 1, 2, 6)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (53, 1, 3, 6)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (54, 1, 4, 6)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (55, 1, 5, 6)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (56, 1, 6, 6)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (57, 1, 7, 6)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (58, 1, 8, 6)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (59, 1, 9, 6)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (60, 1, 10, 6)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (61, 1, 1, 7)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (62, 1, 2, 7)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (63, 1, 3, 7)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (64, 1, 4, 7)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (65, 1, 5, 7)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (66, 1, 6, 7)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (67, 1, 7, 7)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (68, 1, 8, 7)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (69, 1, 9, 7)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (70, 1, 10, 7)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (71, 1, 1, 8)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (72, 1, 2, 8)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (73, 1, 3, 8)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (74, 1, 4, 8)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (75, 1, 5, 8)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (76, 1, 6, 8)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (77, 1, 7, 8)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (78, 1, 8, 8)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (79, 1, 9, 8)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (80, 1, 10, 8)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (81, 1, 1, 9)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (82, 1, 2, 9)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (83, 1, 3, 9)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (84, 1, 4, 9)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (85, 1, 5, 9)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (86, 1, 6, 9)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (87, 1, 7, 9)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (88, 1, 8, 9)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (89, 1, 9, 9)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (90, 1, 10, 9)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (91, 1, 1, 10)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (92, 1, 2, 10)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (93, 1, 3, 10)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (94, 1, 4, 10)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (95, 1, 5, 10)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (96, 1, 6, 10)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (97, 1, 7, 10)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (98, 1, 8, 10)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (99, 1, 9, 10)
GO
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (100, 1, 10, 10)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (101, 2, 1, 1)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (102, 2, 1, 2)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (103, 2, 1, 3)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (104, 2, 1, 4)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (105, 2, 1, 5)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (106, 2, 1, 6)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (107, 2, 1, 7)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (108, 2, 1, 8)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (109, 2, 1, 9)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (110, 2, 1, 10)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (111, 2, 2, 1)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (112, 2, 2, 2)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (113, 2, 2, 3)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (114, 2, 2, 4)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (115, 2, 2, 5)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (116, 2, 2, 6)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (117, 2, 2, 7)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (118, 2, 2, 8)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (119, 2, 2, 9)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (120, 2, 2, 10)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (121, 2, 3, 1)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (122, 2, 3, 2)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (123, 2, 3, 3)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (124, 2, 3, 4)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (125, 2, 3, 5)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (126, 2, 3, 6)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (127, 2, 3, 7)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (128, 2, 3, 8)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (129, 2, 3, 9)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (130, 2, 3, 10)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (131, 2, 4, 1)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (132, 2, 4, 2)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (133, 2, 4, 3)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (134, 2, 4, 4)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (135, 2, 4, 5)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (136, 2, 4, 6)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (137, 2, 4, 7)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (138, 2, 4, 8)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (139, 2, 4, 9)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (140, 2, 4, 10)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (141, 2, 5, 1)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (142, 2, 5, 2)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (143, 2, 5, 3)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (144, 2, 5, 4)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (145, 2, 5, 5)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (146, 2, 5, 6)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (147, 2, 5, 7)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (148, 2, 5, 8)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (149, 2, 5, 9)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (150, 2, 5, 10)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (151, 2, 6, 1)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (152, 2, 6, 2)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (153, 2, 6, 3)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (154, 2, 6, 4)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (155, 2, 6, 5)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (156, 2, 6, 6)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (157, 2, 6, 7)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (158, 2, 6, 8)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (159, 2, 6, 9)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (160, 2, 6, 10)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (161, 2, 7, 1)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (162, 2, 7, 2)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (163, 2, 7, 3)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (164, 2, 7, 4)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (165, 2, 7, 5)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (166, 2, 7, 6)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (167, 2, 7, 7)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (168, 2, 7, 8)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (169, 2, 7, 9)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (170, 2, 7, 10)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (171, 2, 8, 1)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (172, 2, 8, 2)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (173, 2, 8, 3)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (174, 2, 8, 4)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (175, 2, 8, 5)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (176, 2, 8, 6)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (177, 2, 8, 7)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (178, 2, 8, 8)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (179, 2, 8, 9)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (180, 2, 8, 10)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (181, 3, 1, 1)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (182, 3, 2, 1)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (183, 3, 3, 1)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (184, 3, 4, 1)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (185, 3, 5, 1)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (186, 3, 6, 1)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (187, 3, 7, 1)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (188, 3, 8, 1)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (189, 3, 9, 1)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (190, 3, 10, 1)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (191, 3, 11, 1)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (192, 3, 12, 1)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (193, 3, 1, 2)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (194, 3, 2, 2)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (195, 3, 3, 2)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (196, 3, 4, 2)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (197, 3, 5, 2)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (198, 3, 6, 2)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (199, 3, 7, 2)
GO
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (200, 3, 8, 2)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (201, 3, 9, 2)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (202, 3, 10, 2)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (203, 3, 11, 2)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (204, 3, 12, 2)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (205, 3, 1, 3)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (206, 3, 2, 3)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (207, 3, 3, 3)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (208, 3, 4, 3)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (209, 3, 5, 3)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (210, 3, 6, 3)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (211, 3, 7, 3)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (212, 3, 8, 3)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (213, 3, 9, 3)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (214, 3, 10, 3)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (215, 3, 11, 3)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (216, 3, 12, 3)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (217, 3, 1, 4)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (218, 3, 2, 4)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (219, 3, 3, 4)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (220, 3, 4, 4)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (221, 3, 5, 4)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (222, 3, 6, 4)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (223, 3, 7, 4)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (224, 3, 8, 4)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (225, 3, 9, 4)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (226, 3, 10, 4)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (227, 3, 11, 4)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (228, 3, 12, 4)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (229, 3, 1, 5)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (230, 3, 2, 5)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (231, 3, 3, 5)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (232, 3, 4, 5)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (233, 3, 5, 5)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (234, 3, 6, 5)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (235, 3, 7, 5)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (236, 3, 8, 5)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (237, 3, 9, 5)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (238, 3, 10, 5)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (239, 3, 11, 5)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (240, 3, 12, 5)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (241, 3, 1, 6)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (242, 3, 2, 6)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (243, 3, 3, 6)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (244, 3, 4, 6)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (245, 3, 5, 6)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (246, 3, 6, 6)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (247, 3, 7, 6)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (248, 3, 8, 6)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (249, 3, 9, 6)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (250, 3, 10, 6)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (251, 3, 11, 6)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (252, 3, 12, 6)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (253, 3, 1, 7)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (254, 3, 2, 7)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (255, 3, 3, 7)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (256, 3, 4, 7)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (257, 3, 5, 7)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (258, 3, 6, 7)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (259, 3, 7, 7)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (260, 3, 8, 7)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (261, 3, 9, 7)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (262, 3, 10, 7)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (263, 3, 11, 7)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (264, 3, 12, 7)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (265, 3, 1, 8)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (266, 3, 2, 8)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (267, 3, 3, 8)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (268, 3, 4, 8)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (269, 3, 5, 8)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (270, 3, 6, 8)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (271, 3, 7, 8)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (272, 3, 8, 8)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (273, 3, 9, 8)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (274, 3, 10, 8)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (275, 3, 11, 8)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (276, 3, 12, 8)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (277, 3, 1, 9)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (278, 3, 2, 9)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (279, 3, 3, 9)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (280, 3, 4, 9)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (281, 3, 5, 9)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (282, 3, 6, 9)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (283, 3, 7, 9)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (284, 3, 8, 9)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (285, 3, 9, 9)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (286, 3, 10, 9)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (287, 3, 11, 9)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (288, 3, 12, 9)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (289, 3, 1, 10)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (290, 3, 2, 10)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (291, 3, 3, 10)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (292, 3, 4, 10)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (293, 3, 5, 10)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (294, 3, 6, 10)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (295, 3, 7, 10)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (296, 3, 8, 10)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (297, 3, 9, 10)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (298, 3, 10, 10)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (299, 3, 11, 10)
GO
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (300, 3, 12, 10)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (301, 4, 1, 1)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (302, 4, 1, 2)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (303, 4, 1, 3)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (304, 4, 1, 4)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (305, 4, 1, 5)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (306, 4, 1, 6)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (307, 4, 1, 7)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (308, 4, 1, 8)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (309, 4, 1, 9)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (310, 4, 1, 10)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (311, 4, 2, 1)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (312, 4, 2, 2)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (313, 4, 2, 3)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (314, 4, 2, 4)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (315, 4, 2, 5)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (316, 4, 2, 6)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (317, 4, 2, 7)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (318, 4, 2, 8)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (319, 4, 2, 9)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (320, 4, 2, 10)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (321, 4, 3, 1)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (322, 4, 3, 2)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (323, 4, 3, 3)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (324, 4, 3, 4)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (325, 4, 3, 5)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (326, 4, 3, 6)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (327, 4, 3, 7)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (328, 4, 3, 8)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (329, 4, 3, 9)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (330, 4, 3, 10)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (331, 4, 4, 1)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (332, 4, 4, 2)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (333, 4, 4, 3)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (334, 4, 4, 4)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (335, 4, 4, 5)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (336, 4, 4, 6)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (337, 4, 4, 7)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (338, 4, 4, 8)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (339, 4, 4, 9)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (340, 4, 4, 10)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (341, 4, 5, 1)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (342, 4, 5, 2)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (343, 4, 5, 3)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (344, 4, 5, 4)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (345, 4, 5, 5)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (346, 4, 5, 6)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (347, 4, 5, 7)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (348, 4, 5, 8)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (349, 4, 5, 9)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (350, 4, 5, 10)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (351, 5, 1, 1)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (352, 5, 1, 2)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (353, 5, 1, 3)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (354, 5, 1, 4)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (355, 5, 1, 5)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (356, 5, 1, 6)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (357, 5, 1, 7)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (358, 5, 1, 8)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (359, 5, 1, 9)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (360, 5, 1, 10)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (361, 5, 2, 1)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (362, 5, 2, 2)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (363, 5, 2, 3)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (364, 5, 2, 4)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (365, 5, 2, 5)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (366, 5, 2, 6)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (367, 5, 2, 7)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (368, 5, 2, 8)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (369, 5, 2, 9)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (370, 5, 2, 10)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (371, 5, 3, 1)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (372, 5, 3, 2)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (373, 5, 3, 3)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (374, 5, 3, 4)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (375, 5, 3, 5)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (376, 5, 3, 6)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (377, 5, 3, 7)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (378, 5, 3, 8)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (379, 5, 3, 9)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (380, 5, 3, 10)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (381, 5, 4, 1)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (382, 5, 4, 2)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (383, 5, 4, 3)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (384, 5, 4, 4)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (385, 5, 4, 5)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (386, 5, 4, 6)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (387, 5, 4, 7)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (388, 5, 4, 8)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (389, 5, 4, 9)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (390, 5, 4, 10)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (391, 5, 5, 1)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (392, 5, 5, 2)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (393, 5, 5, 3)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (394, 5, 5, 4)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (395, 5, 5, 5)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (396, 5, 5, 6)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (397, 5, 5, 7)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (398, 5, 5, 8)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (399, 5, 5, 9)
GO
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (400, 5, 5, 10)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (401, 5, 6, 1)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (402, 5, 6, 2)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (403, 5, 6, 3)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (404, 5, 6, 4)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (405, 5, 6, 5)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (406, 5, 6, 6)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (407, 5, 6, 7)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (408, 5, 6, 8)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (409, 5, 6, 9)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (410, 5, 6, 10)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (411, 5, 7, 1)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (412, 5, 7, 2)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (413, 5, 7, 3)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (414, 5, 7, 4)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (415, 5, 7, 5)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (416, 5, 7, 6)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (417, 5, 7, 7)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (418, 5, 7, 8)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (419, 5, 7, 9)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (420, 5, 7, 10)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (421, 5, 8, 1)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (422, 5, 8, 2)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (423, 5, 8, 3)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (424, 5, 8, 4)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (425, 5, 8, 5)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (426, 5, 8, 6)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (427, 5, 8, 7)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (428, 5, 8, 8)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (429, 5, 8, 9)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (430, 5, 8, 10)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (431, 5, 9, 1)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (432, 5, 9, 2)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (433, 5, 9, 3)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (434, 5, 9, 4)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (435, 5, 9, 5)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (436, 5, 9, 6)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (437, 5, 9, 7)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (438, 5, 9, 8)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (439, 5, 9, 9)
INSERT [dbo].[Seats] ([Id], [HallId], [RowNumber], [SeatNumber]) VALUES (440, 5, 9, 10)
SET IDENTITY_INSERT [dbo].[Seats] OFF
GO
SET IDENTITY_INSERT [dbo].[Sessions] ON 

INSERT [dbo].[Sessions] ([Id], [MovieId], [HallId], [DateTime], [Price]) VALUES (1, 1, 1, CAST(N'2026-01-29T18:00:00.000' AS DateTime), CAST(450.00 AS Decimal(10, 2)))
INSERT [dbo].[Sessions] ([Id], [MovieId], [HallId], [DateTime], [Price]) VALUES (2, 2, 3, CAST(N'2026-01-29T20:00:00.000' AS DateTime), CAST(650.00 AS Decimal(10, 2)))
INSERT [dbo].[Sessions] ([Id], [MovieId], [HallId], [DateTime], [Price]) VALUES (3, 3, 1, CAST(N'2026-01-29T12:00:00.000' AS DateTime), CAST(350.00 AS Decimal(10, 2)))
INSERT [dbo].[Sessions] ([Id], [MovieId], [HallId], [DateTime], [Price]) VALUES (4, 4, 1, CAST(N'2026-01-30T15:00:00.000' AS DateTime), CAST(380.00 AS Decimal(10, 2)))
INSERT [dbo].[Sessions] ([Id], [MovieId], [HallId], [DateTime], [Price]) VALUES (5, 5, 2, CAST(N'2026-01-30T19:00:00.000' AS DateTime), CAST(500.00 AS Decimal(10, 2)))
INSERT [dbo].[Sessions] ([Id], [MovieId], [HallId], [DateTime], [Price]) VALUES (6, 6, 4, CAST(N'2026-01-30T22:00:00.000' AS DateTime), CAST(550.00 AS Decimal(10, 2)))
INSERT [dbo].[Sessions] ([Id], [MovieId], [HallId], [DateTime], [Price]) VALUES (7, 2, 2, CAST(N'2026-01-30T17:00:00.000' AS DateTime), CAST(600.00 AS Decimal(10, 2)))
INSERT [dbo].[Sessions] ([Id], [MovieId], [HallId], [DateTime], [Price]) VALUES (8, 1, 5, CAST(N'2026-01-31T18:00:00.000' AS DateTime), CAST(420.00 AS Decimal(10, 2)))
INSERT [dbo].[Sessions] ([Id], [MovieId], [HallId], [DateTime], [Price]) VALUES (9, 3, 3, CAST(N'2026-01-31T14:00:00.000' AS DateTime), CAST(520.00 AS Decimal(10, 2)))
INSERT [dbo].[Sessions] ([Id], [MovieId], [HallId], [DateTime], [Price]) VALUES (10, 4, 1, CAST(N'2026-01-31T11:00:00.000' AS DateTime), CAST(350.00 AS Decimal(10, 2)))
INSERT [dbo].[Sessions] ([Id], [MovieId], [HallId], [DateTime], [Price]) VALUES (11, 5, 2, CAST(N'2026-01-31T21:00:00.000' AS DateTime), CAST(480.00 AS Decimal(10, 2)))
INSERT [dbo].[Sessions] ([Id], [MovieId], [HallId], [DateTime], [Price]) VALUES (12, 6, 1, CAST(N'2026-02-01T20:00:00.000' AS DateTime), CAST(400.00 AS Decimal(10, 2)))
INSERT [dbo].[Sessions] ([Id], [MovieId], [HallId], [DateTime], [Price]) VALUES (13, 2, 3, CAST(N'2026-02-01T16:00:00.000' AS DateTime), CAST(700.00 AS Decimal(10, 2)))
INSERT [dbo].[Sessions] ([Id], [MovieId], [HallId], [DateTime], [Price]) VALUES (14, 3, 2, CAST(N'2026-02-01T13:00:00.000' AS DateTime), CAST(420.00 AS Decimal(10, 2)))
INSERT [dbo].[Sessions] ([Id], [MovieId], [HallId], [DateTime], [Price]) VALUES (15, 1, 4, CAST(N'2026-02-01T19:00:00.000' AS DateTime), CAST(580.00 AS Decimal(10, 2)))
SET IDENTITY_INSERT [dbo].[Sessions] OFF
GO
SET IDENTITY_INSERT [dbo].[Tickets] ON 

INSERT [dbo].[Tickets] ([Id], [SessionId], [UserId], [SeatId], [PurchaseDate], [PricePaid], [Status]) VALUES (1, 1, 1, 15, CAST(N'2026-01-29T10:07:31.973' AS DateTime), CAST(450.00 AS Decimal(10, 2)), N'Active')
INSERT [dbo].[Tickets] ([Id], [SessionId], [UserId], [SeatId], [PurchaseDate], [PricePaid], [Status]) VALUES (2, 1, 2, 16, CAST(N'2026-01-29T10:07:31.973' AS DateTime), CAST(450.00 AS Decimal(10, 2)), N'Active')
INSERT [dbo].[Tickets] ([Id], [SessionId], [UserId], [SeatId], [PurchaseDate], [PricePaid], [Status]) VALUES (3, 2, 3, 45, CAST(N'2026-01-29T10:07:31.973' AS DateTime), CAST(650.00 AS Decimal(10, 2)), N'Active')
INSERT [dbo].[Tickets] ([Id], [SessionId], [UserId], [SeatId], [PurchaseDate], [PricePaid], [Status]) VALUES (4, 3, 1, 22, CAST(N'2026-01-29T10:07:31.973' AS DateTime), CAST(350.00 AS Decimal(10, 2)), N'Active')
INSERT [dbo].[Tickets] ([Id], [SessionId], [UserId], [SeatId], [PurchaseDate], [PricePaid], [Status]) VALUES (5, 4, 2, 78, CAST(N'2026-01-29T10:07:31.973' AS DateTime), CAST(380.00 AS Decimal(10, 2)), N'Active')
INSERT [dbo].[Tickets] ([Id], [SessionId], [UserId], [SeatId], [PurchaseDate], [PricePaid], [Status]) VALUES (6, 5, 3, 12, CAST(N'2026-01-29T10:07:31.973' AS DateTime), CAST(500.00 AS Decimal(10, 2)), N'Cancelled')
INSERT [dbo].[Tickets] ([Id], [SessionId], [UserId], [SeatId], [PurchaseDate], [PricePaid], [Status]) VALUES (7, 6, 1, 33, CAST(N'2026-01-29T10:07:31.973' AS DateTime), CAST(550.00 AS Decimal(10, 2)), N'Used')
INSERT [dbo].[Tickets] ([Id], [SessionId], [UserId], [SeatId], [PurchaseDate], [PricePaid], [Status]) VALUES (8, 8, 2, 44, CAST(N'2026-01-29T10:07:31.973' AS DateTime), CAST(420.00 AS Decimal(10, 2)), N'Active')
INSERT [dbo].[Tickets] ([Id], [SessionId], [UserId], [SeatId], [PurchaseDate], [PricePaid], [Status]) VALUES (9, 9, 3, 55, CAST(N'2026-01-29T10:07:31.973' AS DateTime), CAST(520.00 AS Decimal(10, 2)), N'Active')
INSERT [dbo].[Tickets] ([Id], [SessionId], [UserId], [SeatId], [PurchaseDate], [PricePaid], [Status]) VALUES (10, 9, 5, 224, CAST(N'2026-02-22T16:58:53.303' AS DateTime), CAST(520.00 AS Decimal(10, 2)), N'Active')
INSERT [dbo].[Tickets] ([Id], [SessionId], [UserId], [SeatId], [PurchaseDate], [PricePaid], [Status]) VALUES (11, 9, 5, 188, CAST(N'2026-02-22T17:02:11.393' AS DateTime), CAST(520.00 AS Decimal(10, 2)), N'Active')
INSERT [dbo].[Tickets] ([Id], [SessionId], [UserId], [SeatId], [PurchaseDate], [PricePaid], [Status]) VALUES (12, 1, 5, 55, CAST(N'2026-02-22T17:03:50.613' AS DateTime), CAST(450.00 AS Decimal(10, 2)), N'Active')
INSERT [dbo].[Tickets] ([Id], [SessionId], [UserId], [SeatId], [PurchaseDate], [PricePaid], [Status]) VALUES (13, 14, 5, 144, CAST(N'2026-02-22T17:24:07.413' AS DateTime), CAST(420.00 AS Decimal(10, 2)), N'Active')
INSERT [dbo].[Tickets] ([Id], [SessionId], [UserId], [SeatId], [PurchaseDate], [PricePaid], [Status]) VALUES (14, 6, 5, 335, CAST(N'2026-02-22T17:29:59.387' AS DateTime), CAST(550.00 AS Decimal(10, 2)), N'Active')
INSERT [dbo].[Tickets] ([Id], [SessionId], [UserId], [SeatId], [PurchaseDate], [PricePaid], [Status]) VALUES (15, 5, 5, 117, CAST(N'2026-02-22T17:32:04.173' AS DateTime), CAST(500.00 AS Decimal(10, 2)), N'Active')
SET IDENTITY_INSERT [dbo].[Tickets] OFF
GO
SET IDENTITY_INSERT [dbo].[Users] ON 

INSERT [dbo].[Users] ([Id], [Email], [Password], [FullName], [BirthDate], [CreatedAt]) VALUES (1, N'user1@mail.ru', N'234872', N'Погуляев Андрей', CAST(N'2008-01-20' AS Date), CAST(N'2026-01-29T10:07:31.810' AS DateTime))
INSERT [dbo].[Users] ([Id], [Email], [Password], [FullName], [BirthDate], [CreatedAt]) VALUES (2, N'user2@mail.ru', N'234873', N'Васильев Кирилл', CAST(N'2012-08-12' AS Date), CAST(N'2026-01-29T10:07:31.810' AS DateTime))
INSERT [dbo].[Users] ([Id], [Email], [Password], [FullName], [BirthDate], [CreatedAt]) VALUES (3, N'user3@mail.ru', N'123456', N'Иван Иванов', CAST(N'2005-01-31' AS Date), CAST(N'2026-01-29T10:07:31.810' AS DateTime))
INSERT [dbo].[Users] ([Id], [Email], [Password], [FullName], [BirthDate], [CreatedAt]) VALUES (4, N'admin@cinema.ru', N'admin', N'Админ', CAST(N'2001-01-01' AS Date), CAST(N'2026-01-29T10:07:31.810' AS DateTime))
INSERT [dbo].[Users] ([Id], [Email], [Password], [FullName], [BirthDate], [CreatedAt]) VALUES (5, N'ilia@gmail.com', N'123456', N'Илья Муромец', CAST(N'2007-08-29' AS Date), CAST(N'2026-02-22T16:41:33.893' AS DateTime))
SET IDENTITY_INSERT [dbo].[Users] OFF
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__Genres__737584F69461921B]    Script Date: 29.03.2026 11:33:32 ******/
ALTER TABLE [dbo].[Genres] ADD UNIQUE NONCLUSTERED 
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__Halls__737584F6132E59D4]    Script Date: 29.03.2026 11:33:32 ******/
ALTER TABLE [dbo].[Halls] ADD UNIQUE NONCLUSTERED 
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__Users__A9D1053498E787CF]    Script Date: 29.03.2026 11:33:32 ******/
ALTER TABLE [dbo].[Users] ADD UNIQUE NONCLUSTERED 
(
	[Email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Movies] ADD  DEFAULT ((0.0)) FOR [Rating]
GO
ALTER TABLE [dbo].[Tickets] ADD  DEFAULT (getdate()) FOR [PurchaseDate]
GO
ALTER TABLE [dbo].[Tickets] ADD  DEFAULT ('Active') FOR [Status]
GO
ALTER TABLE [dbo].[Users] ADD  DEFAULT (getdate()) FOR [CreatedAt]
GO
ALTER TABLE [dbo].[MovieGenres]  WITH CHECK ADD FOREIGN KEY([GenreId])
REFERENCES [dbo].[Genres] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[MovieGenres]  WITH CHECK ADD FOREIGN KEY([MovieId])
REFERENCES [dbo].[Movies] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Seats]  WITH CHECK ADD FOREIGN KEY([HallId])
REFERENCES [dbo].[Halls] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Sessions]  WITH CHECK ADD FOREIGN KEY([HallId])
REFERENCES [dbo].[Halls] ([Id])
GO
ALTER TABLE [dbo].[Sessions]  WITH CHECK ADD FOREIGN KEY([MovieId])
REFERENCES [dbo].[Movies] ([Id])
GO
ALTER TABLE [dbo].[Tickets]  WITH CHECK ADD FOREIGN KEY([SeatId])
REFERENCES [dbo].[Seats] ([Id])
GO
ALTER TABLE [dbo].[Tickets]  WITH CHECK ADD FOREIGN KEY([SessionId])
REFERENCES [dbo].[Sessions] ([Id])
GO
ALTER TABLE [dbo].[Tickets]  WITH CHECK ADD FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
GO

```

