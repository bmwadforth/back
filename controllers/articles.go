package controllers

import (
	"bmwadforth/common"
	"bmwadforth/models"
	"encoding/json"
	"fmt"
	"net/http"
)

func GetArticles(w http.ResponseWriter, r *http.Request) {

	db := common.DBConnect()
	fmt.Println("# Querying bmwadforth.articles")
	rows, err := db.Query("SELECT * FROM bmwadforth.articles")
	checkErr(err)

	var temp models.Blog

	for rows.Next() {
		var id int
		var blog_title string
		var blog_content string
		var blog_description string
		var blog_views string
		var blog_created string

		err = rows.Scan(&id, &blog_title, &blog_content, &blog_description, &blog_views, &blog_created)
		checkErr(err)
		temp = models.Blog{BlogId: id,
			BlogTitle:       blog_title,
			BlogContent:     blog_content,
			BlogDescription: blog_description,
			BlogViews:       blog_views,
			BlogCreatedAt:   blog_created}
	}

	data, _ := json.Marshal(temp)
	w.Header().Add("content-type", "application/json")
	w.Write(data)
}

func NewArticle(w http.ResponseWriter, r *http.Request) {

	db := common.DBConnect()
	fmt.Println("# Inserting bmwadforth.articles")

	var temp models.Blog

	decoder := json.NewDecoder(r.Body)
	err := decoder.Decode(&temp)
	checkErr(err)
	defer r.Body.Close()

	var lastInsertId int
	sqlStmt := `INSERT INTO bmwadforth.articles (blog_title, blog_content, blog_description) VALUES ($1, $2, $3) RETURNING ID;`
	err = db.QueryRow(sqlStmt, temp.BlogTitle, temp.BlogContent, temp.BlogDescription).Scan(&lastInsertId)
	checkErr(err)

	res := models.HttpRepsonse{Status: 200, Error: false, Message: "Successfully created blog record", Data: lastInsertId}
	data, _ := json.Marshal(res)

	w.Header().Add("content-type", "application/json")
	w.Write(data)
}

func checkErr(err error) {
	if err != nil {
		panic(err)
	}
}
