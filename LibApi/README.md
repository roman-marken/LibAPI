# LibraryApi

Простий ASP.NET Core Web API для домашнього завдання з пагінації, фільтрації та сортування книг.

## Запуск

1. Перевірити connection string у `appsettings.json`.
2. Застосувати міграції:

```bash
dotnet ef database update
```

3. Запустити API:

```bash
dotnet run
```

Swagger буде доступний за адресою `http://localhost:5176/swagger`.

## Приклади запитів

```http
GET /api/books?page=1&size=5
GET /api/books?page=2&size=2
GET /api/books?genre=Dystopia&sortBy=year&sortOrder=desc
GET /api/books?search=orwell&sortBy=date&sortOrder=desc
```

`GET /api/books` повертає `PagedResult<BookDto>`, а поля `PurchasePrice`, `InternalNotes` та `StorageLocation` не потрапляють у відповідь.
