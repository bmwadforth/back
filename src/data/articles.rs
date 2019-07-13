use postgres::{Connection, TlsMode};
use crate::models::articles::Article;
use crate::models::db_connection_details::DbConnectionDetails;
use rocket_contrib::json::Json;

pub fn get_articles() -> Vec<Article>{
    let conn = DbConnectionDetails::new();
    let conn = Connection::connect(format!("postgres://{}:{}@{}:{}/{}", conn.username, conn.password, conn.host, conn.port, conn.database_name), TlsMode::None).unwrap();
    let mut articles: Vec<Article> = Vec::new();
    for row in &conn.query("SELECT * FROM articles;", &[]).unwrap() {
        let article = Article {
            id: row.get(0),
            name: row.get(1),
            content_key: row.get(2),
            tags: row.get(3),
            created: row.get(4),
            content: None
        };
        articles.push(article);
    }

    articles
}

pub fn get_article(name: String) -> Option<Article> {
    let conn = DbConnectionDetails::new();
    let conn = Connection::connect(format!("postgres://{}:{}@{}:{}/{}", conn.username, conn.password, conn.host, conn.port, conn.database_name), TlsMode::None).unwrap();
    let name = str::replace(&name, "-", " ");
    let rows = &conn.query("SELECT * FROM articles WHERE name ILIKE $1;", &[&name]).unwrap();

    if rows.len() > 1 {
        return None;
    } else if rows.len() == 0 {
        return None;
    } else {
        let row = rows.get(0);
        let article = Article {
            id: row.get(0),
            name: row.get(1),
            content_key: row.get(2),
            tags: row.get(3),
            created: row.get(4),
            content: None
        };
        return Some(article)
    }
}