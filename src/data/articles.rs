use postgres::{Connection, TlsMode};
use crate::models::articles::Article;
use rocket_contrib::json::Json;

pub fn get_articles() -> Vec<Article>{
    let conn = Connection::connect("postgres://postgres:password@localhost:5432/bmwadforth", TlsMode::None).unwrap();
    let mut articles: Vec<Article> = Vec::new();
    for row in &conn.query("SELECT * FROM articles;", &[]).unwrap() {
        let article = Article {
            id: row.get(0),
            name: row.get(1),
            content: row.get(2),
            tags: row.get(3),
            created: row.get(4)
        };
        articles.push(article);
    }

    articles
}