using System;
using System.Collections.Generic;

/// <summary>
/// A generic state machine that provides a framework for entering, updating, and exiting states.
/// </summary>
public class StateMachine
{
    // Dictionaries that hold the enter, update, and exit functions for each state.
    Dictionary<int, System.Action> enterStateFuncLookup = new Dictionary<int, System.Action>();
    Dictionary<int, System.Action> updateStateFuncLookup = new Dictionary<int, System.Action>();
    Dictionary<int, System.Action> exitStateFuncLookup = new Dictionary<int, System.Action>();

    // Property of current state
    public int CurrentState => currentState;

    // Fields for currentState, nextState, and the total number of states.
    int currentState, nextState, numOfStates;

    // constructor without parameter
    public StateMachine()
    {
        currentState = -1;
        nextState = -1;
    }

    // constructor with one parameter: numOfState
    public StateMachine(int numOfState)
    {
        this.numOfStates = numOfState;
        currentState = -1;
        nextState = -1;
    }

    /// <summary>
    /// Set the three functions (enter, update, and exit) for this state.
    /// </summary>
    /// <param name="state"></param>
    /// <param name="enterStateFunc"></param>
    /// <param name="updateStateFunc"></param>
    /// <param name="exitStateFunc"></param>
    public void SetStateFunctions(int state, System.Action enterStateFunc, System.Action updateStateFunc, System.Action exitStateFunc)
    {
        if (ValidState(state)) // Validate if the input state is valid or not.
        {
            enterStateFuncLookup[state] = enterStateFunc;
            updateStateFuncLookup[state] = updateStateFunc;
            exitStateFuncLookup[state] = exitStateFunc;
        }
    }

    /// <summary>
    /// Set the total number of states this state machine has.
    /// </summary>
    /// <param name="numOfStates"></param>
    public void SetNumOfStates(int numOfStates)
    {
        this.numOfStates = numOfStates;
    }

    /// <summary>
    /// Set the enter function for this state.
    /// </summary>
    /// <param name="state"></param>
    /// <param name="func"></param>
    public void SetEnterStateFunc(int state, System.Action func)
    {
        if (ValidState(state))
        {
            enterStateFuncLookup[state] = func;
        }
    }

    /// <summary>
    /// Set the update function for this state.
    /// </summary>
    /// <param name="state"></param>
    /// <param name="func"></param>
    public void SetUpdateStateFunc(int state, System.Action func)
    {
        if (ValidState(state))
        {
            updateStateFuncLookup[state] = func;
        }
    }

    /// <summary>
    /// Set the exit function for this state.
    /// </summary>
    /// <param name="state"></param>
    /// <param name="func"></param>
    public void SetExitStateFunc(int state, System.Action func)
    {
        if (ValidState(state))
        {
            exitStateFuncLookup[state] = func;
        }
    }

    /// <summary>
    /// Instruct the state machine to enter the given state.
    /// </summary>
    /// <param name="nextState">The desired next state</param>
    public void SetState(int nextState)
    {
        if (ValidState(nextState))
        {
            this.nextState = nextState;
        }
    }

    /// <summary>
    /// This function is called by the object that owns this state machine. 
    /// It’s typically invoked every frame to update the current state.
    /// </summary>
    public void UpdateState()
    {
        if (currentState != nextState) // check if the state changed
        {
            // [C1_QUIZ]
            // Student implement (15 mins)
            ///
            // only do this if currentState is a valid one, do the exitState function
            // change currentState
            // do the enter state function
            ///

            if (currentState >= 0)
            {
                exitStateFuncLookup[currentState]();
            }
            currentState = nextState;
            enterStateFuncLookup[currentState]();
        }
        if (currentState >= 0)
        {
            updateStateFuncLookup[currentState](); // check if current state is a valid one
        }
    }

    /// <summary>
    /// Check if the state is a valid one
    /// </summary>
    /// <param name="state"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    private bool ValidState(int state)
    {
        if (state < numOfStates && state >= 0)
        {
            return true;
        }
        else
        {
            throw new ArgumentOutOfRangeException(nameof(state), state,
                "Not a valid state. You may need to initialize numOfStates by calling SetNumOfStates(int numOfStates) first.");
        }
    }
}
