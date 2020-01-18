package service

import (
	"errors"
	"github.com/bmwadforth/jwt"
	"log"
	"time"
)

func NewToken() ([]byte, error) {
	key := []byte("MAKE_THIS_A_COMPLEX_KEY_BEFORE_PROD_LOL")

	claims := jwt.NewClaimSet()
	claims.Add(string(jwt.Audience), "bmwadforth")
	claims.Add(string(jwt.Subject), "bmwadforth")
	claims.Add(string(jwt.IssuedAt), time.Now())
	claims.Add(string(jwt.ExpirationTime), time.Now().Add(time.Hour * 24))

	token, err := jwt.New(jwt.HS256, claims, key); if err != nil {
		log.Println(err)
		return nil, errors.New("JWT cannot be created")
	}

	tokenBytes, err := token.Encode(); if err != nil {
		log.Println(err)
		return nil, errors.New("JWT cannot be encoded")
	}

	return tokenBytes, nil
}

func ValidateToken(tokenString string) (bool, error) {
	key := []byte("MAKE_THIS_A_COMPLEX_KEY_BEFORE_PROD_LOL")

	token, err := jwt.Parse(tokenString, key)
	if err != nil {
		log.Println(err)
		return false, errors.New("JWT string is invalid")
	}

	_, err = jwt.Validate(token)
	if err != nil {
		log.Println(err)
		return false, errors.New("JWT is invalid")
	}

	return true, nil
}