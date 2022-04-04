# Three Body Game
* [itch](https://avivajpeyi.itch.io/three-body)
* [ludum-dare-50](https://ldjam.com/events/ludum-dare/50/three-body)

As gravity pulls the three bodies close, destruction is inevitable. Try to weave between the two celestial bodies that are hurtling towards you to score points and delay the inevitable. 


## To train ML model:

We are using reinforcement learning with immitation-learing to initialise the training. 

1. Start `ML training` scene
2. Open terminal and source ML-agents python env
3. cd to project dir and run: ` mlagents-learn Assets/ML-Agents/Configs/threeBody
.yaml --run-id <runid>`
4. To view training progress run:  `tensorboard --logdir results/`

Current bot is not performming too well -- maybe the rewards are not adjusted? Maybe I should train using immitation learning for the first 5k steps, then switch over to training with reinforement learning? 

