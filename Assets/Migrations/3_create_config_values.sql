
CREATE TABLE config_values (
    id INT PRIMARY KEY,
    config_id INT NOT NULL,
    value VARCHAR(255) NOT NULL,
    description VARCHAR(255) NULL,
    FOREIGN KEY (config_id) REFERENCES config(id)
);