import { DataTypes, Model, Sequelize } from 'sequelize';
import logger from '../util/logger';

const sequelize = new Sequelize(
  process.env.BMWADFORTH_DB_NAME || 'postgres',
  process.env.BMWADFORTH_DB_USER || 'postgres',
  process.env.BMWADFORTH_DB_PASSWORD || 'password',
  {
    dialect: 'postgres',
    host: process.env.BMWADFORTH_DB_HOST || 'postgres',
    logging: false,
    define: {
      freezeTableName: true,
      schema: 'public',
    },
    dialectOptions: {
      options: {
        requestTimeout: 3000,
      },
    },
  }
);

export class Article extends Model {}
Article.init(
  {
    // Model attributes are defined here
    title: {
      type: DataTypes.STRING,
      allowNull: false,
      unique: true,
    },
    description: {
      type: DataTypes.STRING,
      allowNull: false,
    },
    content: {
      type: DataTypes.TEXT,
      allowNull: false,
    },
    tags: {
      type: DataTypes.ARRAY(DataTypes.STRING(128)),
    },
    image: {
      type: DataTypes.STRING,
    },
    likes: {
      type: DataTypes.BIGINT,
      defaultValue: 0,
    },
  },
  {
    // Other model options go here
    sequelize,
    modelName: 'articles',
  }
);

export class Comment extends Model {}
Comment.init(
  {
    // Model attributes are defined here
    ip: {
      type: DataTypes.INET,
      allowNull: false,
    },
    comment: {
      type: DataTypes.STRING,
      allowNull: false,
    },
  },
  {
    // Other model options go here
    sequelize,
    modelName: 'comments',
  }
);

Article.hasMany(Comment);
Comment.belongsTo(Article, {});

export function connectDatabase() {
  return new Promise(async (resolve, reject) => {
    try {
      const res = await sequelize.authenticate();
      logger.info('Database Connected');
      //await sequelize.sync({ force: true });
      //logger.info('Database Synchronised');
      resolve(res);
    } catch (error) {
      console.error(error);
      reject(error);
    }
  });
}

export default sequelize;
