using Emgu.CV;
using Google.Apis.Sheets.v4.Data;
using System;
using static Tensorflow.Binding;
using IronPython;

namespace DlgMenuDemo
{
    public class pong_ai
    {

        //hyper params
        double ACTIONS = 3; //up,down, stay  define our learning rate
        double GAMMA = 0.99;
        //for updating our gradient or training over time
        double INITIAL_EPSILON = 1.0;
        double FINAL_EPSILON = 0.05;
        //how many frames to anneal epsilon
        double EXPLORE = 500000;
        double OBSERVE = 50000;
        //store our experiences, the size of it
        double REPLAY_MEMORY = 500000;
        //batch size to train on
        double BATCH = 100;

        object[] createGraph()
        {

            var W_conv1 = tf.Variable(tf.zeros(new Tensorflow.TensorShape(new int[] { 8, 8, 4, 32 })));
            var b_conv1 = tf.Variable(tf.zeros(new Tensorflow.TensorShape(new int[] { 32 })));

            var W_conv2 = tf.Variable(tf.zeros(new Tensorflow.TensorShape(new int[] { 4, 4, 32, 64 })));
            var b_conv2 = tf.Variable(new Tensorflow.TensorShape(new int[] { 64 }));

            var W_conv3 = tf.Variable(tf.zeros(new Tensorflow.TensorShape(new int[] { 3, 3, 64, 64 })));
            var b_conv3 = tf.Variable(new Tensorflow.TensorShape(new int[] { 64 }));

            var W_fc4 = tf.Variable(tf.zeros(new Tensorflow.TensorShape(new int[] { 3136, 784 })));
            var b_fc4 = tf.Variable(new Tensorflow.TensorShape(new int[] { 784 }));

            new Tensorflow.TensorShape(new int[] { 784, (int)ACTIONS });
            var W_fc5 = tf.Variable(tf.zeros(new Tensorflow.TensorShape(new int[] { 784, (int)ACTIONS })));
            var b_fc5 = tf.Variable(tf.zeros(new Tensorflow.TensorShape(new int[] { (int)ACTIONS })));

            var s = tf.placeholder(Tensorflow.TF_DataType.TF_FLOAT, new Tensorflow.TensorShape(new int[] { (int)None, 84, 84, 4 }));



            var conv1 = tf.nn.relu(tf.nn.conv2d(s, W_conv1, strides: new int[] { 1, 4, 4, 1 }, padding: "VALID") + b_conv1);

            var conv2 = tf.nn.relu(tf.nn.conv2d(conv1, W_conv2, strides: new int[] { 1, 2, 2, 1 }, padding: "VALID") + b_conv2);

            var conv3 = tf.nn.relu(tf.nn.conv2d(conv2, W_conv3, strides: new int[] { 1, 1, 1, 1 }, padding: "VALID") + b_conv3);

            var conv3_flat = tf.reshape(conv3, new int[] { -1, 3136 });

            var fc4 = tf.nn.relu(tf.matmul(conv3_flat, W_fc4) + b_fc4);


            var fc5 = tf.matmul(fc4, W_fc5) + b_fc5;

            return new object[] { s, fc5 };
        }


        public void tensor_main()
        {


            createGraph();
        }
    }
}
