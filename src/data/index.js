import { DataTypes, Model, Sequelize } from 'sequelize';
import logger from '../util/logger';

const dbName = process.env.BMWADFORTH_DB_NAME || 'postgres';
const dbUser = process.env.BMWADFORTH_DB_USER || 'postgres';
const dbPass = process.env.BMWADFORTH_DB_PASSWORD || 'postgres';
const dbHost = process.env.BMWADFORTH_DB_HOST || 'localhost'

const sequelize = new Sequelize(`postgres://${dbUser}:${dbPass}@${dbHost}/${dbName}`, {
  dialect: 'postgres',
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
})

export class Article extends Model {}
Article.init(
  {
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

export class Project extends Model {}
Project.init(
  {
    title: {
      type: DataTypes.STRING,
      allowNull: false,
      unique: true,
    },
    description: {
      type: DataTypes.STRING,
      allowNull: false,
    },
    read_me: {
      type: DataTypes.STRING,
      allowNull: false,
    },
    repository: {
      type: DataTypes.STRING,
      allowNull: false,
    },
  },
  {
    // Other model options go here
    sequelize,
    modelName: 'projects',
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
