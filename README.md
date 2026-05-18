# HomeLibrary — Домашняя библиотека

Веб-приложение для управления домашней библиотекой книг. Выполнено в рамках тестового задания.

<div align="center">
  <h3>Главный экран</h3>
  <img src="Screenshots/main.png" style="width: 100%; max-width: 500px; border-radius: 12px;">
  <br><br>
  
  <h3>Добавить книгу</h3>
  <img src="Screenshots/add-book.png" style="width: 100%; max-width: 500px; border-radius: 12px;">
  <br><br>
  
  <h3>Изменить книгу</h3>
  <img src="Screenshots/edit-book.png" style="width: 100%; max-width: 500px; border-radius: 12px;">
  <br><br>
  
  <h3>Удалить книгу</h3>
  <img src="Screenshots/delete-book.png" style="width: 100%; max-width: 500px; border-radius: 12px;">
</div>

## Возможности
- Просмотр списка книг
- Добавление, редактирование, удаление книг
- Хранимые процедуры для CRUD
- Оглавление в формате HTML

## Хранимые процедуры

| Процедура | Тип | Описание |
|-----------|-----|----------|
| GetAllBooks | SELECT | Возвращает все книги, отсортированные по названию |
| GetBookById | SELECT | Возвращает одну книгу по ID |
| InsertBook | INSERT | Добавляет новую книгу |
| UpdateBook | UPDATE | Обновляет существующую книгу |
| DeleteBook | DELETE | Удаляет книгу по ID |

## Стек технологий

| Слой | Технология |
|------|------------|
| База данных | MS SQL Server 2022 (T-SQL) |
| ORM | Entity Framework Core |
| Бэкенд | ASP.NET Core MVC (C#) |
| Контейнеризация | Docker + docker-compose |
| Интерфейс | Razor Pages (HTML + CSS) |

## Архитектура(MVC):

- **Model:** `Book.cs`   Класс книги (соответствует таблице books)
- **Views:**
  - `Index.cshtml`     Страница списка книг
  - `Create.cshtml`    Форма добавления книги
  - `Edit.cshtml`      Форма редактирования
  - `Delete.cshtml`    Подтверждение удаления
- **Controllers:** `BooksController.cs`    Обработка CRUD-операций


## Схема взаимодействия
Пользователь (браузер) -> http://localhost:5123/Books -> Program.cs -> BooksController -> AppDbContext -> MS SQL Server (Docker, порт 1433)


## Работа с XML/HTML

Поле `content_html` хранит оглавление книги в формате NVARCHAR(MAX). Для редактирования используется стандартное текстовое поле с возможностью ввода HTML-тегов. Вычисляемый столбец `content_xml` автоматически преобразует содержимое в XML-тип, что позволяет выполнять XPath-запросы.
<div align="center">
  <h3>Главный экран</h3>
  <img src="Screenshots/XML.png" style="width: 100%; max-width: 500px; border-radius: 12px;">
  <br><br>
</div>

### Выборка данных из XML
Примеры SQL-запросов для работы с XML-полем находятся в файле `SQL/xml_query_example.sql`:
- Извлечение заголовков глав через XPath
- Подсчёт количества глав
- Поиск по содержимому оглавления

## Запуск
Клонировать репозиторий:
        git clone https://github.com/Kekesruin/HomeLibrary.git
        cd HomeLibrary
   
1. Запустить MS SQL Server:
```   
   docker compose up -d --build
```
2. Выполнить sqlcmd команды (Создание таблицы, создание хранимых процедур, заполнение таблицы, xml запросы):
```
sudo docker exec -i $(sudo docker ps -qf "name=mssql") /opt/mssql-tools18/bin/sqlcmd -S localhost -U SA -P "HomeLibrary123!" -C -i /docker-entrypoint-initdb.d/create_table.sql
sudo docker exec -i $(sudo docker ps -qf "name=mssql") /opt/mssql-tools18/bin/sqlcmd -S localhost -U SA -P "HomeLibrary123!" -d HomeLibrary -C -i /docker-entrypoint-initdb.d/procedures.sql
sudo docker exec -i $(sudo docker ps -qf "name=mssql") /opt/mssql-tools18/bin/sqlcmd -S localhost -U SA -P "HomeLibrary123!" -d HomeLibrary -C -i /docker-entrypoint-initdb.d/seed_data.sql
```
## Вывод xml запросов в терминале: 
```
sudo docker exec -i $(sudo docker ps -qf "name=mssql") /opt/mssql-tools18/bin/sqlcmd -S localhost -U SA -P "HomeLibrary123!" -d HomeLibrary -C -Y 80 -i /docker-entrypoint-initdb.d/xml_query_example.sql

```

## Примечания
- MS SQL Server работает в Docker, не требует установки на хост
- При `docker compose down` данные сохраняются (том `mssql-data`)
- При `docker compose down -v` данные удаляются - нужно заново выполнить SQL-скрипты
- Проект разработан на Linux с VS Code (вместо Visual Studio)

