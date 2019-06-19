use rocket_contrib::json::Json;
use crate::models::articles::Article;
use crate::data::articles::get_articles;
use crate::data::articles::get_article;
use rocket::Response;

#[get("/api/articles")]
pub fn fetch_articles() -> Json<Vec<Article>> {
    Json(get_articles())
}

#[get("/api/article?<name>")]
pub fn fetch_article(name: String) -> Option<Json<Article>> {
    match get_article(name) {
        Some(article) => return Some(Json(article)),
        None => return None
    }
}

