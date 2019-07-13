use std::env;
use std::error::Error;

pub struct DbConnectionDetails {
    pub database_name: String,
    pub username: String,
    pub password: String,
    pub port: u32,
    pub host: String
}

impl DbConnectionDetails {
    pub fn new() -> DbConnectionDetails {
        let database_name = env::var("BMWADFORTH_DATABASE").expect("BMWADFORTH_DATABASE Invalid");
        let username = env::var("BMWADFORTH_USERNAME").expect("BMWADFORTH_USERNAME Invalid");
        let password = env::var("BMWADFORTH_PASSWORD").expect("BMWADFORTH_PASSWORD Invalid");
        let host = env::var("BMWADFORTH_HOST").expect("BMWADFORTH_HOST Invalid");
        let port = env::var("BMWADFORTH_PORT").expect("BMWADFORTH_PORT Invalid");
        let port = port.parse().expect("BMWADFORTH_PORT Invalid");

        return DbConnectionDetails {
            database_name,
            username,
            password,
            host,
            port
        };
    }
}