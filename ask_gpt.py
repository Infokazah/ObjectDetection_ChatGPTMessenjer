import g4f
import clr

def ask_gpt(message: str) -> str:
    response = g4f.ChatCompletion.create(
        model=g4f.models.gpt_35_turbo_0613,
        messages=[{"role": "user", "content": message}]
    )
    
    return response




