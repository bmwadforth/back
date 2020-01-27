package controllers

import (
	"github.com/bmwadforth/back/src/controllers/mutation"
	"github.com/bmwadforth/back/src/controllers/query"
	"github.com/gin-gonic/gin"
	"github.com/graphql-go/graphql"
	"github.com/graphql-go/handler"
)

var Schema, _ = graphql.NewSchema(
	graphql.SchemaConfig{
		Query:        query.QueryType,
		Mutation:     mutation.MutationType,
		//Subscription: subscription.SubscriptionType,
	},
)

func Handler() gin.HandlerFunc {
	h := handler.New(&handler.Config{
		Schema: &Schema,
		Pretty: true,
		GraphiQL: true,
	})

	return func(c *gin.Context) {
		/*
		token, err := service.TokenFromHeader(c.Request.Header.Get("Authorization")); if err != nil {
			log.Println(err)
		}

		ctx := context.WithValue(context.Background(), "token", token)

		h.ContextHandler(ctx, c.Writer, c.Request)*/
		h.ServeHTTP(c.Writer, c.Request)
	}
}