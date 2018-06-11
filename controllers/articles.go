package controllers

import (
	"bmwadforth/common"
	"bmwadforth/models"
	"encoding/json"
	"fmt"
	"net/http"
	"bmwadforth/util"
)

func GetArticles(w http.ResponseWriter, r *http.Request) {
	db := common.DBConnect()
	w.Header().Add("access-control-allow-origin", "http://localhost:3000")
	fmt.Println("# Querying bmwadforth.articles")
	rows, err := db.Query("SELECT * FROM bmwadforth.articles")
	util.CheckErr(err)

	var blogs []models.Blog
	defer rows.Close()

	for rows.Next() {
		var id int
		var blog_title string
		var blog_content string
		var blog_description string
		var blog_views string
		var blog_image string
		var blog_created string

		err = rows.Scan(&id, &blog_title, &blog_content, &blog_description, &blog_views, &blog_image, &blog_created)
		util.CheckErr(err)
		temp := models.Blog{BlogId: id,
			BlogTitle:       blog_title,
			BlogContent:     blog_content,
			BlogDescription: blog_description,
			BlogViews:       blog_views,
			BlogImage:       blog_image,
			BlogCreatedAt:   blog_created}
		blogs = append(blogs, temp)
	}

	data, _ := json.Marshal(blogs)
	db.Close();
	w.Header().Add("content-type", "application/json")
	w.Write(data)
}

func GetArticle(w http.ResponseWriter, r *http.Request) {
	db := common.DBConnect()
	w.Header().Add("access-control-allow-origin", "http://localhost:3000")
	fmt.Println("# Querying bmwadforth.articles")

	whichId := r.FormValue("id")

	sqlStmt := `SELECT * FROM bmwadforth.articles WHERE id = $1`
	row := db.QueryRow(sqlStmt, whichId)

	blog  := models.Blog {}

	row.Scan(&blog.BlogId, &blog.BlogTitle, &blog.BlogContent, &blog.BlogDescription, &blog.BlogViews, &blog.BlogImage, &blog.BlogCreatedAt)

	data, _ := json.Marshal(blog)
	db.Close();
	w.Header().Add("content-type", "application/json")
	w.Write(data)
}

func NewArticle(w http.ResponseWriter, r *http.Request) {

	db := common.DBConnect()
	fmt.Println("# Inserting bmwadforth.articles")

	var temp models.Blog

	decoder := json.NewDecoder(r.Body)
	err := decoder.Decode(&temp)
	util.CheckErr(err)
	defer r.Body.Close()

	var lastInsertId int
	sqlStmt := `INSERT INTO bmwadforth.articles (blog_title, blog_content, blog_description) VALUES ($1, $2, $3) RETURNING ID;`
	err = db.QueryRow(sqlStmt, temp.BlogTitle, temp.BlogContent, temp.BlogDescription).Scan(&lastInsertId)
	util.CheckErr(err)

	res := models.HttpRepsonse{Status: 200, Error: false, Message: "Successfully created blog record", Data: lastInsertId}
	data, _ := json.Marshal(res)
	db.Close();

	w.Header().Add("content-type", "application/json")
	w.Write(data)
}
