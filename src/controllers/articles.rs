use rocket_contrib::json::Json;
use crate::models::articles::Article;
use crate::data::articles::get_articles;
use rocket::Response;

#[get("/api/articles")]
pub fn fetch_articles() -> Json<Vec<Article>> {
    Json(get_articles())
}