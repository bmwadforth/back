package service

import "github.com/bmwadforth/jwt"

func NewToken() (string, error) {
	claims := jwt.NewClaimSet()

}

func ValidateToken() (bool, error) {

}