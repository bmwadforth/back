import express from 'express';
import buildResponse from '../util/response';
import { Article, Comment } from '../data';
import { authorize } from '../middleware/authorization';

const router = express.Router();

router.get('/', async (req, res, next) => {
  try {
    const articles = await Article.findAll({
      include: {
        model: Comment,
        as: 'comments',
      },
    });
    res.json(buildResponse('Fetched articles successfully', articles));
  } catch (err) {
    next(err);
  }
});

router.put('/', authorize, async (req, res, next) => {
  try {
    const { title, description, content, tags, image } = req.body;
    await Article.create({ title, description, content, tags, image });
    res.json(buildResponse('Article created successfully', null));
  } catch (err) {
    next(err);
  }
});

export default router;
