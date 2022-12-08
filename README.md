# AI Agent Perception
This respository is meant for demo purposes only.  It is not suitable for production and it is not guaranteed to run in Unity.

## Overview
When an AI Agent needs to make decisions on what to do next, the chosen behavior aught to reflect what that Agent understands.  If the Agent doesn't know critical information, they should not be able to use that information when deciding what to do next.  This creates continuity for the player, and deepens rewards for the player if they can out-wit an Agent.

The demo in this repository shows an implementation of a system to record, store, and query information that an Agent knows.  The general concept is that the Agent contains a list of Perceptors that query the environment at a configurable rate.  The Perceptors create Perception Records when something of note is percieved.  These records contain metadata about the perception, such as time, location, and other filterable information.  Methods for querying this data is shown, and there is also support for sharing knowledge between AI Agents to simulate their ability to communicate.

A common use-case for this type of system is for implementing enemy agents for a stealth action game like Metal Gear Solid.  The agents can see and hear, and they can communicate with each other.  If an agent sees the player, then they will take an action such as shoot towrard the location or converge on the last known location of the player (even if the player has moved from since the last time they were seen).

The example in this demo shows a Sight-based use-case for an Agent, and the ability to shoot in the direction of the position they last saw the player.  Other types of Perceptors can be created that focus on how as specific perction should be recorded in your experience, such as AudioPerceptors, TremorPerceptors, TelepathicPerceptors, AlarmPerceptors, etc.  When deciding on a next action, the Decision-Making algorithm can query this single interface to determine what information it knows.

## Technology Used
* Unity AI
* Unity Physics

## Concepts Shown
* A decoupled abstract framework for scalable solution building
* Phyiscs-based Agent Perception
* Coroutine usage
* Custom edtior gizmos

