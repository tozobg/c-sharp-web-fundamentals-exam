# Transaction System - Console Application

Тransaction system written in **C# (.NET 8)** for interview task.  

---

## 📌 Features

### Core Operations
- Create Account  
- Deposit  
- Withdraw  
- Transfer between accounts  

### Architecture Highlights
- **Separation of concerns** across:
  - `TransactionSystem.Core`
  - `TransactionSystem.Data` (EF Core SQLite/SQLite in memory)
  - `TransactionSystem.Data.InMemory` (In memory objects)
  - `TransactionSystem.Models`
  - `TransactionSystem` Console

- **Unit of Work Pattern**  
- **Repository Pattern**  
- **DTO-based communication** 

### Two Data Providers
- **SQLite provider** (default + in memory)
- **In-Memory provider** (easy switching)

### NUnit Unit Tests
- Uses the **In-Memory Data Layer** for isolated testing  
- Covers:
  - Account creation
  - Deposit / Withdraw
  - Transfers
  - Error cases

---

## Database Structure (SQLite)

![Database Diagram](assets/db_schhema.png)

### **Account**
| Column        | Type    |
|---------------|---------|
| Id            | int PK  |
| AccountNumber | int     | (Unique)
| FullName      | string  |
| Balance       | decimal |

### **Deposit**
| Column    | Type    |
|-----------|---------|
| Id        | int PK  |
| AccountId | FK → Account.Id |
| Money     | decimal |
| Date      | text    | (Datetime)

### **Withdraw**
| Column    | Type    |
|-----------|---------|
| Id        | int PK |
| AccountId | FK → Account.Id |
| Money     | decimal |
| Date      | text    | (Datetime)

### **Transfer**
| Column        | Type    |
|---------------|---------|
| Id            | int PK |
| FromAccountId | FK → Account.Id |
| ToAccountId   | FK → Account.Id |
| Money         | decimal |
| Date          | text    | (Datetime)

---

## Switching Between SQLite & In-Memory

Inside **StartUp.cs**:

```csharp
bool useInMemory = false; // set true to use InMemory instead of SQLite
bool useInMemorySQLite = false;  // set true to use SQLite but in memory variant
```


## StartUp 
TransactionSystem -> StartUp.cs

Enjoy exploring the project!