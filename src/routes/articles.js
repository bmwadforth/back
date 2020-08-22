import express from 'express';
import buildResponse from '../util/response';
import Article from '../data/article';
import { authorize } from '../middleware/authorization';

const router = express.Router();

router.get('/', async (req, res, next) => {
  const articles = await Article.findAll();
  res.json(buildResponse('Fetched articles successfully', articles));
});

router.put('/', authorize, async (req, res, next) => {
  try {
    const { title, description, tags, image } = req.body;
    // TODO: Validate request body
    await Article.create({ title, description, tags, image });
    res.json(buildResponse('Article created successfully', null));
  } catch (err) {
    next(err);
  }
});

export default router;
