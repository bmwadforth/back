package database

import (
	"github.com/bmwadforth/back/src/models"
	"github.com/lib/pq"
)

func GetArticles() ([]models.Article, error) {
	articles := make([]models.Article, 0, 10)

	db := OpenDatabase()
	defer db.Database.Close()

	rows, err := db.Database.Query("SELECT * FROM ARTICLES")
	if err != nil {
		return nil, err
	}

	for rows.Next() {
		article := models.Article{Author: models.User{}, Meta: models.ArticleMeta{Likes: 0, Views: 0}}
		err := rows.Scan(&article.ID, &article.Title, &article.Description, pq.Array(&article.Tags), &article.Author.ID, &article.Created, &article.Data, &article.Meta, &article.Author.Username, &article.Author.Created)
		if err != nil {
			return nil, err
		}
		articles = append(articles, article)
	}

	return articles, nil
}
