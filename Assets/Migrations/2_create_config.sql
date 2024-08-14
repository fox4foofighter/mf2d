CREATE TABLE config (
    id INT PRIMARY KEY,
    name VARCHAR(255) UNIQUE NOT NULL,
    description VARCHAR(255) NULL
);
