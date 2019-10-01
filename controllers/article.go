package controllers

import (
	"bmwadforth/database"
	"bmwadforth/service"
	"bmwadforth/util"
	"github.com/gin-gonic/gin"
	"net/http"
)

func GetArticles(c *gin.Context) {
	articles, err := database.GetArticles()
	if err != nil {
		response := util.NewResponse(http.StatusInternalServerError, "Failed To Fetch Articles").AddError(err)
		c.JSON(response.Status, response)
		return
	}

	response := util.NewResponse(http.StatusOK, "Fetched Articles").SetData(articles)
	c.JSON(response.Status, response)
}

func GetArticle(c *gin.Context) {
	id := c.Param("id")
	article, err := database.GetArticle(id)
	if err != nil {
		response := util.NewResponse(http.StatusInternalServerError, "Failed To Fetch Article").AddError(err)
		c.JSON(response.Status, response)
		return
	}

	articleBytes, err := service.GetArticle(article.FileRef)
	if err  != nil {
		response := util.NewResponse(http.StatusInternalServerError, "Failed To Fetch Article").AddError(err)
		c.JSON(response.Status, response)
		return
	}

	article.FileContent = string(articleBytes)
	response := util.NewResponse(http.StatusOK, "Fetched Article").SetData(article)
	c.JSON(response.Status, response)
}

func NewArticle(c *gin.Context) {
	c.JSON(200, gin.H{
		"message": "pong",
	})
}