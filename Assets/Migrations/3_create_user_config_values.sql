DROP TABLE IF EXISTS user_config_values;

CREATE TABLE user_config_values (
    user_id INT NOT NULL,
    config_id INT NOT NULL,
    value VARCHAR(255) NOT NULL,
    PRIMARY KEY (user_id, config_id),
    FOREIGN KEY (user_id) REFERENCES users(id),
    FOREIGN KEY (config_id) REFERENCES config(id)
);