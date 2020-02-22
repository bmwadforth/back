package types

import "github.com/graphql-go/graphql"

var ProjectType = graphql.NewObject(
	graphql.ObjectConfig{
		Name: "ProjectType",
		Fields: graphql.Fields{
			"id": &graphql.Field{
				Type: graphql.Int,
			},
			"title": &graphql.Field{
				Type: graphql.String,
			},
			"description": &graphql.Field{
				Type: graphql.String,
			},
			"tags": &graphql.Field{
				Type: graphql.NewList(graphql.String),
			},
			"github": &graphql.Field{
				Type: graphql.String,
			},
			"created": &graphql.Field{
				Type: graphql.DateTime,
			},
		},
	},
)