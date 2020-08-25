import express from 'express';
import buildResponse from '../util/response';
import { Article, Comment } from '../data';
import { authorize } from '../middleware/authorization';

const router = express.Router();

router.get('/:articleID', async (req, res, next) => {
  const { articleID } = req.params;
  if (!articleID) {
    res.status(400).json(buildResponse('Article ID must be supplied in query'));
    return;
  }

  try {
    const article = await Article.findOne({
      include: {
        model: Comment,
        as: 'comments',
      },
      where: {
        id: articleID,
      },
    });
    res.json(buildResponse('Fetched article successfully', article));
  } catch (err) {
    next(err);
  }
});

router.get('/', async (req, res, next) => {
  try {
    const articles = await Article.findAll({
      include: {
        model: Comment,
        as: 'comments',
      },
      attributes: {
        exclude: ['content'],
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

router.put('/:articleID/like', async (req, res, next) => {
  try {
    const { articleID } = req.params;
    await Article.increment('likes', {
      where: {
        id: articleID,
      },
    });
    res.json(buildResponse('Article liked successfully', null));
  } catch (err) {
    next(err);
  }
});

router.put('/:articleID/unlike', async (req, res, next) => {
  try {
    const { articleID } = req.params;
    await Article.decrement('likes', {
      where: {
        id: articleID,
      },
    });
    res.json(buildResponse('Article unliked successfully', null));
  } catch (err) {
    next(err);
  }
});

export default router;
