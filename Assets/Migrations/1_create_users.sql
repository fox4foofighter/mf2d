DROP TABLE IF EXISTS users;

CREATE TABLE users (
    id INT PRIMARY KEY,
    name VARCHAR(255) NULL,
    state_description VARCHAR(255) NULL,
    last_logined_at DATETIME NULL,
    created_at DATETIME NULL
);