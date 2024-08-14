CREATE TABLE user_config_values (
    user_id INT NOT NULL,
    config_value_id INT NOT NULL,
    PRIMARY KEY (user_id, config_value_id),
    FOREIGN KEY (user_id) REFERENCES users(id),
    FOREIGN KEY (config_value_id) REFERENCES config_values(id)
);