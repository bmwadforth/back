#![feature(proc_macro_hygiene, decl_macro)]
#[macro_use]
extern crate rocket;
extern crate serde;
extern crate postgres;
extern crate time;
extern crate uuid;

mod controllers;
mod data;
mod models;

#[get("/")]
fn index() -> &'static str {
    "Hello, world!"
}

fn main() {
    rocket::ignite().mount("/", routes![index, controllers::articles::fetch_articles]).launch();
}