A Test Project to Reproduce an issue with Nhibernate using custom types

Repro create a local Database named test in MSSQLSERVER

        CREATE TABLE Product (
            Id INT IDENTITY(1,1) PRIMARY KEY,
            Name NVARCHAR(MAX),
            ValidFrom DATE NOT NULL DEFAULT CAST(GETDATE() AS DATE),
            ValidUntil DATE
        );
        
        INSERT INTO Product (Name, ValidUntil)
        VALUES 
        ('Product A', '2025-12-31'),
        ('Product B', NULL),
        ('Product C', '2026-06-30'),
        ('Product D', NULL),
        ('Product E', '2025-09-15'),
        ('Product F', NULL),
        ('Product G', '2027-01-01'),
        ('Product H', NULL),
        ('Product I', '2025-08-01'),
        ('Product J', NULL);

  Create a products table and insert some products using the script above. Change your connectionstring in the Program.cs file to use your credentials/login method

