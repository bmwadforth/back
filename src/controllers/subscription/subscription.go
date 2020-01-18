package subscription

import "github.com/graphql-go/graphql"

var SubscriptionType = graphql.NewObject(
	graphql.ObjectConfig{
		Name: "subscription",
		Fields: graphql.Fields{
		},
	})

